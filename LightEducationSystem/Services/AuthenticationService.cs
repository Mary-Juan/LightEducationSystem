using LightEducationSystem.DataAccess.Repositories;
using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.ViewModels;

namespace LightEducationSystem.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _peopleFilePath;
        private readonly IGenericRepository<Person> _personRepository;

        public AuthenticationService(IConfiguration configuration)
        {
            _peopleFilePath = configuration["FileAddresses:PeopleFilePath"];
            _personRepository = new GenericRepository<Person>(_peopleFilePath);
        }

        public Person? Login(LoginViewModel login)
        {
            List<Person> people = _personRepository.GetAll();
            Person currentUser = people.FirstOrDefault(p => p.Email == login.Email && p.Password == login.Password);
            
            if (currentUser == null)
            {
                return null;
            }

            return currentUser;
            
        }

        public bool Register(RegisterViewModel register)
        {
            try
            {
                if (register.RoleId == 2)
                {
                    Student student = new Student()
                    {
                        Id = _personRepository.GetAll().Count() + 1,
                        UserName = register.UserName,
                        Password = register.Password,
                        Email = register.Email,
                        role = new PersonRole()
                        {
                            Id = 2,
                            Title = "Student"
                        }
                    };

                    _personRepository.Create(student);
                    _personRepository.SaveChanges();
                }
                else if (register.RoleId == 1)
                {
                    Professor professor = new Professor()
                    {
                        Id = _personRepository.GetAll().Count() + 1,
                        UserName = register.UserName,
                        Password = register.Password,
                        Email = register.Email,
                        role = new PersonRole()
                        {
                            Id = 1,
                            Title = "Professor"
                        }
                    };

                    _personRepository.Create(professor);
                    _personRepository.SaveChanges();
                }

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
