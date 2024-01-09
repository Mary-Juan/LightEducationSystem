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
            if (studentId != _currentUser.Id)
            {
                ViewBag.UnAuthorizedError = "You've not been identified. please Login.";
                return RedirectToAction("Login", "Home");
            }

            var student = _studentService.GetStudentDetails;

            if (student == null)
                return NotFound();

            return View(student);
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
                    ViewBag.FullCapacityError = "The capacity of this course is full";
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
        
        public IActionResult GetProfessorDetail(int professorId)
        {
            var professor = _studentService.GetProfessorDetail(professorId);

            if (professor == null)
                return NotFound();

            return View(professor);
        }

        public IActionResult GetProfessorTrainingCourse(int professorId)
        {
            return View(_studentService.GetProfessorTrainingCourse(professorId));
        }

        public IActionResult GetTrainingCourseDetail(int trainingCourseId)
        {
            var trainingCourse = _studentService.GetTrainingCourseDetail(trainingCourseId);

            if (trainingCourse == null)
                return NotFound();

            return View(trainingCourse);
        }
    }
}
