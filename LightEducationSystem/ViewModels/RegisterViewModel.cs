using LightEducationSystem.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LightEducationSystem.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Byte RoleId { get; set; }
    }

}
