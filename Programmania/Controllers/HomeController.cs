using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Programmania.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("Registrate")]
        [HttpPost]
        public IActionResult MakeRegister(string data)
        {
            JObject jObject = JObject.Parse(data);
            string name = jObject["name"].ToString();
            int age = int.Parse(jObject["age"].ToString());
            string nickname = jObject["nickname"].ToString();
            string email = jObject["email"].ToString();
            string password = jObject["password"].ToString();
            return Ok();

            //check if email realy exists (use Utility.EmailCheker.CheckIfExists(email)) 
            if (false)
            {
                return BadRequest();
            }
            else
            {
                return View("Main");
            }
        }

        [Route("Main")]
        [HttpPost]
        public IActionResult Main(string data)
        {
            JObject jObject = JObject.Parse(data);
            string email = jObject["email"].ToString();
            string password = jObject["password"].ToString();

            return Ok();
            //check if email exists in db
            if (false)
            {
                return BadRequest();
            }
            else
            {
                return View();
            }
        }

        [Route("Courses")]
        public IActionResult Courses()
        {
            return View();
        }

        [Route("News")]
        public IActionResult News()
        {
            return View();
        }

        [Route("News/PostNews")]
        public IActionResult PostNews()
        {
            return View();
        }

        [Route("Profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("Codes")]
        public IActionResult Codes()
        {
            return View();
        }

        [Route("Challenges")]
        public IActionResult Challenges()
        {
            return View();
        }

        [Route("Challenges/Game")]
        public IActionResult Game()
        {
            return View();
        }
    }
}
