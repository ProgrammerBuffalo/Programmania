using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Programmania.ViewModels;

namespace Programmania.Controllers
{
    [Route("performance")]
    [Authorize]
    public class PerformanceController : Controller
    {
        [Route("")]
        public IActionResult Perfomance()
        {
            return View("/Views/Home/Performance.cshtml");
        }

        //loading of diagram retrurn Reward[] array and put it into PerformanceViewModel then return it using Json
        [Route("rewards-init")]
        public IActionResult GetPerformanceRewards(string type, System.DateTime date)
        {
            PerformanceViewModel viewModel = new PerformanceViewModel(null);
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
