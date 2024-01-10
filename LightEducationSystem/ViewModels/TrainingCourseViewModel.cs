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
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int RemainingCapacity { get; set; }

        public string? ImageAddress { get; set; } = Directory.GetCurrentDirectory() + "wwwroot" + "imgs" + "DefaultTrainingCourseImage.jpg";
        public IFormFile? Image { get; set; }

        public string? Detail { get; set; }
    }
}
