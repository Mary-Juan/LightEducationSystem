using System.ComponentModel.DataAnnotations;

namespace LightEducationSystem.ViewModels
{
    public class TrainingCourseViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int RemainingCapacity { get; set; }
    }
}
