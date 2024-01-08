using LightEducationSystem.Entities;
using LightEducationSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LightEducationSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly Person _currentUser;

        public StudentController(IStudentService studentService, ICurrentUserService currentUserService)
        {
            _studentService = studentService;
            _currentUser = currentUserService.GetCurrentUser();

        }

        public IActionResult Index(int studentId)
        {
            return View(_studentService.GetStudentDetails);
        }

        public IActionResult GetAllTrainingCourse()
        {
            return View(_studentService.GetAllTrainingCourses());
        }

        public IActionResult RegisterTrainingCourse(int trainingCourseId)
        {
            try
            {
                if (_studentService.RegisterTrainingCourse(_currentUser.Id, trainingCourseId))
                    return RedirectToAction("Index", "Student");
                else
                {
                    ViewData["FullCapacityError"] = "The capacity of this course is full";
                    return RedirectToAction("GetAllTrainingCourse");
                }    
            }catch
            {
                return BadRequest();
            }
        }

        public IActionResult GetStudentCourses()
        {
            return View(_studentService.GetStudentTrainingCourse(_currentUser.Id));
        }
    }
}
