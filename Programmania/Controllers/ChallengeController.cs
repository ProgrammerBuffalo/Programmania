using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Programmania.Attributes;
using Programmania.Models;
using Programmania.Services;
using Programmania.ViewModels;

namespace Programmania.Controllers
{
    [Route("Challenges")]
    [Authorize]
    public class ChallengeController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private IFileService fileService;

        public ChallengeController(DAL.ProgrammaniaDBContext context, IFileService fileService)
        {
            this.dbContext = context;
            this.fileService = fileService;
        }

        [HttpGet]
        public IActionResult GetChallenges()
        {
            var user = HttpContext.Items["User"] as User;

            if (user == null)
                return BadRequest();

            List<ChallengeVM> challenges =
                dbContext.UserChallenges.Where(uc => uc.UserId == user.Id && !uc.IsFinished).
                                         Select(s => new ChallengeVM
                                         {
                                             Id = s.ChallengeId,
                                             Course = s.Challenge.Course.Name,
                                             Date = s.Challenge.Created,
                                             OpponentDescription = new UserShortDescriptionVM
                                             {
                                                 Id = s.UserId,
                                                 Name = s.User.Name,
                                                 Avatar = fileService.GetDocument(dbContext.Documents
                                                                     .FirstOrDefault(d => d.StreamId == s.User.ImageId).Path)
                                             }
                                         }).ToList();
            return View(challenges);
        }

        [HttpPost]
        public async Task<IActionResult> SendAnswersPacket(Dictionary<int, int> answers)
        {
            User user = HttpContext.Items["User"] as User;
            int? challengeId = HttpContext.Session.GetInt32("challenge");

            if (challengeId == null)
                return BadRequest();

            List<Test> listTests = getChallengeTests(user, challengeId.Value);

            if (listTests == null)
                return BadRequest();

            int counter = 0;
            foreach (var kvp in answers)
            {
                Test test = listTests.FirstOrDefault(t => t.Id == kvp.Key);
                if (test == null)
                    continue;
                
                if(test.Correct == kvp.Value)
                    counter++;
            }

            UserChallenge userChallenge = dbContext.UserChallenges
                .FirstOrDefault(uc => uc.ChallengeId == challengeId.Value
                                    && uc.UserId == user.Id && !uc.IsFinished);

            if(userChallenge != null)
            {
                userChallenge.AnswersCount = counter;
                userChallenge.IsFinished = true;
                dbContext.Update(userChallenge);
                await dbContext.SaveChangesAsync();
            }
            //else
            //{
            //    await dbContext.Database.ExecuteSqlRawAsync("exec dbo.SetChallengeForUsersPROC @creatorId," +
            //        "@opponentId, @challengeId, @creatorAnswersCount", user.Id)
            //}

            return Ok();
        }

        [Route("create-challenge")]
        [HttpPost]
        public async Task<IActionResult> CreateChallenge(int courseId)
        {
            var challengeParam = new SqlParameter
            {
                ParameterName = "challengeId",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            await dbContext.Database.ExecuteSqlRawAsync("exec dbo.GenerateChallengePROC @courseId, @challengeId OUT", courseId, challengeParam);
            return RedirectToAction("accept-challenge", (int)challengeParam.Value);
        }

        [Route("accept-challenge")]
        [HttpPost]
        public IActionResult AcceptChallenge(int challengeId)
        {
            HttpContext.Session.SetInt32("challenge", challengeId);
            return RedirectToAction("tests");
        }

        [Route("tests")]
        [HttpGet]
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

            return View(tests);       
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