using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult SendAnswersPacket(Dictionary<int, int> answers)
        {
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
            return Ok();
        }

    }
}