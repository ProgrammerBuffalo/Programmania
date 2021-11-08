using Microsoft.AspNetCore.Mvc;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using Programmania.Attributes;
using System.Collections.Generic;

namespace Programmania.Controllers
{
    [Route("Performance")]
    //[Authorize]
    public class PerformanceController : Controller
    {
        private IPerformanceService performanceService;

        public PerformanceController(IPerformanceService performanceService)
        {
            this.performanceService = performanceService;
        }

        [HttpGet("")]
        public IActionResult Perfomance()
        {
            return View("/Views/Home/Performance.cshtml");
        }

        //loading of diagram return Reward[] array and put it into PerformanceViewModel then return it using Json
        [Route("rewards-init")]
        public IActionResult GetPerformanceRewards(System.DateTime from, System.DateTime to)
        {
            //var user = HttpContext.Items["User"] as User;
            //Reward[] rewards = null;
            //if (dbContext.Users.Any(ud => ud.HistoryId == user.HistoryId && ud.Id == user.Id))
            //{

            //    string fullPath = dbContext.Documents.FirstOrDefault().Path;
            //    rewards = xmlService.GetNodes(30, fullPath).ToArray();
            //}

            //List<Reward> currRewards = new List<Reward>();
            //foreach (var reward in rewards)
            //{
            //    if (reward.Type == type && reward.Date == date)
            //    {
            //        currRewards.Add(reward);
            //    }
            //}

            //PerformanceViewModel viewModel = new PerformanceViewModel(currRewards.ToArray());
            //return Json(viewModel);
            return Ok();
        }

        //type - type of diagram (day, month, year)
        //date - current date of diagramm
        //using type and date backend must return Reward[] array
        //For Example type=day date=11/9/2021 return all rewards in September
        //For Example type=month date 11/9/2021 return all rewards in 2021 year
        //For Exaple type=week date 11/9/2021 return rewards from 9 to 15 Semptember (7 days in week)
        [Route("rewards")]
        public IActionResult GetRewards(string type, System.DateTime date)
        {
            return Json(null);
        }

        //count - count of rewards to return
        //offset - how much rewards already returned to client
        [Route("more-rewards")]
        public IActionResult GetMoreRewards(int count, int offset)
        {
            User user = HttpContext.Items["User"] as User;
            if (user != null)
            {
                IEnumerable<Reward> rewards = performanceService.GetRewards(user, count, offset);
                return Json(rewards);
            }
            return BadRequest();
        }
    }
}
