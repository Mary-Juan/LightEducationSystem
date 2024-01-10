using System.ComponentModel.DataAnnotations;

namespace LightEducationSystem.ViewModels
{
    public class TrainingCourseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public TimeSpan EndTime { get; set; }

        public TimeSpan? TotalTime { get; set; }

        public int ProfessorId { get; set; }

        public string? ProfessorName { get; set; }

        public int? RemainingCapacity { get; set; }

        public string? ImageName { get; set; } = "DefaultTrainingCourseImage.jpg";
        public IFormFile? Image { get; set; }

        public string? Detail { get; set; }
    }
}
