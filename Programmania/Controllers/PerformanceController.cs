using Microsoft.AspNetCore.Mvc;
using Programmania.Models;
using Programmania.Services.Interfaces;
using Programmania.ViewModels;
using Programmania.Attributes;
using System.Collections.Generic;

namespace Programmania.Controllers
{
    [Route("Performance")]
    [Authorize]
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

        [Route("rewards-init")]
        public IActionResult GetPerformanceRewards(System.DateTime from, System.DateTime to)
        {
            var user = HttpContext.Items["User"] as User;
            if (user != null)
            {
                IEnumerable<Reward> rewards = performanceService.GetRewards(user, from, to);
                PerformanceViewModel performanceVM = new PerformanceViewModel(rewards);
                return Json(performanceVM);
            }
            else
                return BadRequest();
        }

        [Route("rewards")]
        public IActionResult GetRewards(string type, System.DateTime from, System.DateTime to)
        {
            var user = HttpContext.Items["User"] as User;
            if (user != null)
            {
                IEnumerable<Reward> rewards = performanceService.GetRewards(user, from, to);
                return Json(rewards);
            }
            else
                return BadRequest();
        }

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
