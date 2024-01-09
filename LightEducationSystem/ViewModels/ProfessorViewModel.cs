using System.ComponentModel.DataAnnotations;

namespace LightEducationSystem.ViewModels
{
    public class ProfessorViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} is required.")]
        public string UserName { get; set; }

       public List<TrainingCourseViewModel> TrainingCourses { get; set; } = new List<TrainingCourseViewModel>();
    }
}
