using System.ComponentModel.DataAnnotations;

namespace LightEducationSystem.ViewModels
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string UserName { get; set; }

        public List<StudentCardViewModel> StudentCards { get; set; } = new List<StudentCardViewModel>();
    }
}
