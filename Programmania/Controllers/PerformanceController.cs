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
            User user = HttpContext.Items["User"] as User;
            if (user != null)
            {
                IEnumerable<Reward> rewards = performanceService.GetRewards(user, from, to);

                PerformanceViewModel viewModel = new PerformanceViewModel(rewards);
                return Json(viewModel);
            }
            return BadRequest();
        }

        //method should return rewards that are between from and to dates
        [Route("rewards")]
        public IActionResult GetRewards(System.DateTime from, System.DateTime to)
        {
            User user = HttpContext.Items["User"] as User;
            if (user != null)
            {
                IEnumerable<Reward> rewards = performanceService.GetRewards(user, from, to);
                return Json(rewards);
            }
            return BadRequest();
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
