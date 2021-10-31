using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Programmania.Attributes;
using Programmania.Models;

namespace Programmania.Controllers
{
    [Authorize]
    public class ChallengeController : Controller
    {
        [HttpGet]
        public IActionResult GetChallenges()
        {
            var user = HttpContext.Items["User"] as User;
            return View();
        }

        [HttpPost]
        public IActionResult SendAnswersPacket(Dictionary<int, int> answers)
        {
            return Ok();
        }

        public IActionResult CreateChallenge(int courseId)
        {
            return Ok();
        }

        public IActionResult BeginChallenge(int challengeId)
        {
            return Ok();
        }

    }
}