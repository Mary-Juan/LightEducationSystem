using LightEducationSystem.Entities;

namespace LightEducationSystem.Services.Interfaces
{
    public interface ICurrentUserService
    {
        public Person GetCurrentUser();
        public bool AddCurrentUser(Person person);
    }
}
