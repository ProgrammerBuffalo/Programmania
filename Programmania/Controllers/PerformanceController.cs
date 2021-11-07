using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Programmania.Models;
using Programmania.Services;
using Programmania.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Programmania.Controllers
{
    [Route("performance")]
    [Authorize]
    public class PerformanceController : Controller
    {
        private DAL.ProgrammaniaDBContext dbContext;
        private XMLService xmlService;

        public PerformanceController(DAL.ProgrammaniaDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        [Route("")]
        public IActionResult Perfomance()
        {
            return View("/Views/Home/Performance.cshtml");
        }

        //loading of diagram return Reward[] array and put it into PerformanceViewModel then return it using Json
        [Route("rewards-init")]
        public IActionResult GetPerformanceRewards(string type, System.DateTime date)
        {
            var user = HttpContext.Items["User"] as User;
            Reward[] rewards = null;
            if (dbContext.Users.Any(ud => ud.HistoryId == user.HistoryId && ud.Id == user.Id))
            {

                string fullPath = dbContext.Documents.FirstOrDefault().Path;
                rewards = xmlService.GetNodes(30, fullPath).ToArray();
            }

            List<Reward> currRewards = new List<Reward>();
            foreach (var reward in rewards)
            {
                if (reward.Type == type && reward.Date == date)
                {
                    currRewards.Add(reward);
                }
            }

            PerformanceViewModel viewModel = new PerformanceViewModel(currRewards.ToArray());
            return Json(viewModel);
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
            return Json(null);
        }
    }
}
