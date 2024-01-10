using LightEducationSystem.Entities;
using LightEducationSystem.Services;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LightEducationSystem.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _professorService;
        private readonly Person _currentUser;

        public ProfessorController(IProfessorService professorService, ICurrentUserService currentUserService)
        {
            _professorService = professorService;
            _currentUser = currentUserService.GetCurrentUser();
        }

        public IActionResult Index(int professorId)
        {
            if (professorId != _currentUser.Id)
            {
                ViewBag.UnAuthorizedError = "You've not been identified. please Login.";
                return RedirectToAction("Login", "Home");
            }

            var professor = _professorService.GetProfessorDetail(professorId);

            if (professor == null)
                return NotFound();

            return View(professor);
        }

        public IActionResult AddTrainingCourse()
        {
            return View(new TrainingCourseViewModel()
            {
                Id = _currentUser.Id,
                ProfessorId = _currentUser.Id,
                ProfessorName = _currentUser.UserName
            });
        }

        [HttpPost]
        public IActionResult AddTrainingCourse(TrainingCourseViewModel trainingCourse)
        {
            if(!ModelState.IsValid)
            {
                return View(trainingCourse);
            }

            try
            {
                _professorService.AddTrainingCourse(trainingCourse, _currentUser.Id);
                return RedirectToAction("Index", new { professorId = _currentUser.Id });
            }
            catch
            {
                return BadRequest();
            }
            return View();
        }

        public IActionResult GetStudentsOfTrainingCourse(int trainingCourseId)
        {
            return View(_professorService.GetStudentsOfTrainingCourse(trainingCourseId));
        }

        public IActionResult RateStudent(int courseId, int studentId)
        {
            return View(new StudentCardViewModel()
            {
                TrainingCourseId = courseId,
                StudentId = studentId
            });
        }

        [HttpPost]
        public IActionResult RateStudent( StudentCardViewModel inputCard)
        {
            try
            {
                return View(_professorService.RateStudent(inputCard));
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
