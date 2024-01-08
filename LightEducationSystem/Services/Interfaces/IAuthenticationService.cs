using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.ViewModels;

namespace LightEducationSystem.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Person? Login(LoginViewModel login);
        public bool Register(RegisterViewModel register);
        
    }
}
