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
            return View();
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

        public IActionResult RateStudent(int studentId, int score)
        {
            try
            {
                return View(_professorService.RateStudent(studentId, score));
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
