using LightEducationSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace LightEducationSystem.ViewModels
{
    public class StudentCardViewModel
    {
        public int StudentId { get; set; }

        public string? StudentName { get; set; }

        public int TrainingCourseId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int Score { get; set; }
    }
}
