using LightEducationSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace LightEducationSystem.ViewModels
{
    public class StudentCardViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int TrainingCourseId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int Score { get; set; }
    }
}
