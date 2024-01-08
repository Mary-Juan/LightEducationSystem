using LightEducationSystem.DataAccess.Repositories;
using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services.Interfaces;
using System;

namespace LightEducationSystem.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly string _currentUserFilePath;
        private readonly IGenericRepository<Person> _currentUserRepository;

        public CurrentUserService(IConfiguration configuration)
        {
            _currentUserFilePath = configuration["FileAddresses:CurrentUserFilePath"];
            _currentUserRepository = new GenericRepository<Person>(_currentUserFilePath);
        }

        public bool AddCurrentUser(Person person)
        {
            try
            {
                _currentUserRepository.Delete(person.Id);
                _currentUserRepository.Create(person);
                _currentUserRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Person GetCurrentUser()
        {
            try
            {
               return _currentUserRepository.GetAll().FirstOrDefault(cu => cu.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
