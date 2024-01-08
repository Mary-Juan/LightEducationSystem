using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace LightEducationSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="{0} is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }
    }
}
