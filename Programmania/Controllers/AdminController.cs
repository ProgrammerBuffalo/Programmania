using Microsoft.AspNetCore.Mvc;
using Programmania.Attributes;
using Programmania.DTOs;
using Programmania.Services.Interfaces;

namespace Programmania.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult Admin()
        {
            return View("/Views/Home/Admin.cshtml");
        }

        [AllowAnonymous]
        [HttpPost("Course")]
        public IActionResult AddCourse(CourseDTO dto)
        {
            var res = adminService.AddCourse(dto);
            if (res > 0) return Ok(res);
            else return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("CourseList")]
        public IActionResult CourseList()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("Course")]
        public IActionResult Course()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("Course")]
        public IActionResult UpdateCourse(int courseId, CourseDTO dto)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("Course")]
        public IActionResult DeleteCourse(int courseId)
        {
            return Ok();
        }

        ///
        [AllowAnonymous]
        [HttpPost("Discipline")]
        public IActionResult AddDiscipline(DisciplineDTO dto)
        {
            var res = adminService.AddDiscipline(dto);
            if (res > 0) return Ok(res);
            else return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("DisciplineList")]
        public IActionResult DisciplineList(int courseId)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("Discipline")]
        public IActionResult Discipline()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("Discipline")]
        public IActionResult UpdateDiscipline(int disciplineId, DisciplineDTO course)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("Discipline")]
        public IActionResult DeleteDiscipline(int disciplineId)
        {
            return Ok();
        }

        ///

        [AllowAnonymous]
        [HttpPost("Lesson")]
        public IActionResult AddLesson(AddLessonDTO dto)
        {
            var res = adminService.AddLesson(dto);
            if (res > 0) return Ok(res);
            else return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("LessonList")]
        public IActionResult GetLessonList(int courseId)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("Lesson")]
        public IActionResult GetLesson(int lessonId)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("Lesson")]
        public IActionResult UpdateLesson(int lessonId, AddLessonDTO dto)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("Lesson")]
        public IActionResult DeleteLesson(int lessonId)
        {
            return Ok();
        }

        ///

        [AllowAnonymous]
        [HttpPost("Test")]
        public IActionResult AddDiscipline(AddTestDTO test)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("Test")]
        public IActionResult GetTest(int lessonId)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("Test")]
        public IActionResult UpdateDiscipline(int lessonId, AddTestDTO course)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("Test")]
        public IActionResult DeleteTest(int lessonId)
        {
            return Ok();
        }

        [AllowAnonymous]
        public IActionResult Temp()
        {
            return Ok();
        }
    }
}
