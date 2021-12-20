using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("")]
        public IActionResult Admin()
        {
            return View("/Views/Home/Admin.cshtml");
        }

        [HttpPost("Course")]
        public IActionResult AddCourse(CourseDTO dto)
        {
            var res = adminService.AddCourse(dto);
            if (res > 0) return Ok(res);
            else return BadRequest();
        }

        [HttpGet("CourseList")]
        public IActionResult CourseList()
        {
            return Ok();
        }

        [HttpGet("Course")]
        public IActionResult Course()
        {
            return Ok();
        }

        [HttpPut("Course")]
        public IActionResult UpdateCourse(int courseId, CourseDTO dto)
        {
            return Ok();
        }

        [HttpDelete("Course")]
        public IActionResult DeleteCourse(int courseId)
        {
            return Ok();
        }

        ///

        [HttpPost("Discipline")]
        public IActionResult AddDiscipline(DisciplineDTO dto)
        {
            var res = adminService.AddDiscipline(dto);
            if (res > 0) return Ok(res);
            else return BadRequest();
        }

        [HttpGet("DisciplineList")]
        public IActionResult DisciplineList(int courseId)
        {
            return Ok();
        }

        [HttpGet("Discipline")]
        public IActionResult Discipline()
        {
            return Ok();
        }

        [HttpPut("Discipline")]
        public IActionResult UpdateDiscipline(int disciplineId, DisciplineDTO course)
        {
            return Ok();
        }

        [HttpDelete("Discipline")]
        public IActionResult DeleteDiscipline(int disciplineId)
        {
            return Ok();
        }

        ///

        [HttpPost("Lesson")]
        public IActionResult AddLesson(AddLessonDTO dto)
        {
            var res = adminService.AddLesson(dto);
            if (res > 0) return Ok(res);
            else return BadRequest();
        }

        [HttpGet("LessonList")]
        public IActionResult GetLessonList(int courseId)
        {
            return Ok();
        }

        [HttpGet("Lesson")]
        public IActionResult GetLesson(int lessonId)
        {
            return Ok();
        }

        [HttpPut("Lesson")]
        public IActionResult UpdateLesson(int lessonId, AddLessonDTO dto)
        {
            return Ok();
        }

        [HttpDelete("Lesson")]
        public IActionResult DeleteLesson(int lessonId)
        {
            return Ok();
        }

        ///

        [HttpPost("Test")]
        public IActionResult AddDiscipline(TestDTO test)
        {
            return Ok();
        }

        [HttpGet("Test")]
        public IActionResult GetTest(int lessonId)
        {
            return Ok();
        }

        [HttpPut("Test")]
        public IActionResult UpdateDiscipline(int lessonId, TestDTO course)
        {
            return Ok();
        }

        [HttpDelete("Test")]
        public IActionResult DeleteTest(int lessonId)
        {
            return Ok();
        }

        public IActionResult Temp()
        {
            return Ok();
        }
    }
}
