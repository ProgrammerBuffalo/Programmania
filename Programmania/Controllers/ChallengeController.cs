using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Programmania.Attributes;
using Programmania.DTOs;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ChallengeController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IFileService fileService;
        private IStaticService staticService;

        public ChallengeController(DAL.ProgrammaniaDBContext dbContext, IFileService fileService, IStaticService staticService)
        {
            this.dbContext = dbContext;
            this.fileService = fileService;
            this.staticService = staticService;
        }

        [HttpGet]
        public IActionResult Challange()
        {
            return View("/Views/Home/Challenge.cshtml");
        }

        [HttpGet("Result")]
        public IActionResult Result()
        {
            return View();
        }

        [HttpGet("challenge-stats")]
        public IActionResult GetUserStats()
        {
            var user = HttpContext.Items["User"] as User;

            var stats = dbContext.ChallengeStatistics.FirstOrDefault(s => s.UserId == user.Id);

            if (stats == null)
                return BadRequest();

            return Json(new UserChallengeStatsVM { Wins = stats.Wins, Loses = stats.Loses, Draws = stats.Draws });
        }

        [HttpGet("get-acceptable-challenges")]
        public IActionResult GetAcceptableChallenges()
        {
            var user = HttpContext.Items["User"] as User;

            List<OfferedChallengeVM> offeredChallenges = staticService.GetOfferedChallenges(user, fileService);
            return View(offeredChallenges);
        }

        [HttpGet("get-creatable-challenges")]
        public IActionResult GetCreatableChallenges()
        {
            var user = HttpContext.Items["User"] as User;

            List<PossibleChallengeVM> offeredChallenges = staticService.GetPossibleChallenges(fileService, 10);

            return View(offeredChallenges);
        }

        [HttpPost("send-answers")]
        public async Task<IActionResult> SendAnswersPacket(AnswerDTO[] answers)
        {
            User user = HttpContext.Items["User"] as User;
            int? challengeId = HttpContext.Session.GetInt32("challenge");

            if (challengeId == null)
                return BadRequest();

            List<Test> listTests = getChallengeTests(user, challengeId.Value);

            if (listTests == null)
                return BadRequest();

            int counter = 0;
            foreach (var answer in answers)
            {
                Test test = listTests.FirstOrDefault(t => t.Id == answer.QuestionId);
                if (test == null)
                    continue;

                if (test.Correct == answer.QuestionId)
                    counter++;
            }

            UserChallenge userChallenge = dbContext.UserChallenges
                .FirstOrDefault(uc => uc.ChallengeId == challengeId.Value
                                    && uc.UserId == user.Id && !uc.IsFinished);

            if (userChallenge != null)
            {
                userChallenge.AnswersCount = counter;
                userChallenge.IsFinished = true;
                dbContext.Update(userChallenge);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                int? opponentId = HttpContext.Session.GetInt32("opponent");

                if (opponentId == null)
                    return BadRequest();

                await dbContext.Database.ExecuteSqlRawAsync("exec dbo.SetChallengeForUsersPROC @creatorId," +
                    "@opponentId, @challengeId, @creatorAnswersCount", user.Id, opponentId.Value, challengeId.Value, counter);
            }

            HttpContext.Session.Clear();
            return Ok();
        }

        [HttpPost("create-challenge")]
        public async Task<IActionResult> CreateChallenge(int courseId, int userId)
        {
            var challengeParam = new SqlParameter
            {
                ParameterName = "challengeId",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            await dbContext.Database.ExecuteSqlRawAsync("exec dbo.GenerateChallengePROC @courseId, @challengeId OUT", courseId, challengeParam);
            HttpContext.Session.SetInt32("opponent", userId);

            return RedirectToAction("accept-challenge", (int)challengeParam.Value);
            //return RedirectToAction("accept-challenge", 1);
        }

        [HttpGet("accept-challenge")]
        public IActionResult AcceptChallenge(int challengeId)
        {
            HttpContext.Session.SetInt32("challenge", challengeId);
            return Ok();
        }

        [HttpGet("tests")]
        public IActionResult GetTestsOfChallenge()
        {
            int? challengeId = HttpContext.Session.GetInt32("challenge");
            if (challengeId == null)
                return BadRequest();

            User user = HttpContext.Items["User"] as User;

            List<Test> tests = getChallengeTests(user, challengeId.Value);
            if (tests == null)
                return BadRequest();

            List<TestVM> testsVM = new List<TestVM>();
            foreach (var test in tests)
                testsVM.Add(new TestVM
                {
                    Id = test.Id,
                    Question = test.Question,
                    A1 = test.Answer1,
                    A2 = test.Answer2,
                    A3 = test.Answer3,
                    A4 = test.Answer4
                });

            return Json(tests);

            //List<TestVM> tests = new List<TestVM>()
            //{
            //    new TestVM() { Id = 1, Question = "q1", A1 = "a1", A2 = "b1", A3 = "c1", A4 = "d1"},
            //    new TestVM() { Id = 2, Question = "q2", A1 = "a2", A2 = "b2", A3 = "c2", A4 = "d2"},
            //    new TestVM() { Id = 3, Question = "q3", A1 = "a3", A2 = "b3", A3 = "c3", A4 = "d3"},
            //    new TestVM() { Id = 4, Question = "q4", A1 = "a4", A2 = "b4", A3 = "c4", A4 = "d4"},
            //    new TestVM() { Id = 5, Question = "q5", A1 = "a5", A2 = "b5", A3 = "c5", A4 = "d5"}
            //};
            //return Json(tests);
        }

        private List<Test> getChallengeTests(User user, int challengeId)
        {
            List<Test> tests = dbContext.UserChallenges.FirstOrDefault(uc => uc.ChallengeId == challengeId &&
                                                    uc.UserId == user.Id && !uc.IsFinished)?.Challenge.Tests.ToList();

            if (tests == null)
                return null;

            return tests;
        }

    }
}