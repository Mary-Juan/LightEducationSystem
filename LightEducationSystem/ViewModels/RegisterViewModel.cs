using LightEducationSystem.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LightEducationSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string RoleId { get; set; }
    }

}
