using LightEducationSystem.DataAccess.Repositories;
using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.ViewModels;

namespace LightEducationSystem.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly string _professorFilePath;

        private readonly IStudentRepository _studentRepository;
        private readonly string _studentFilePath;

        public AuthenticationService(IConfiguration configuration)
        {
            _professorFilePath = configuration["FileAddresses:ProfessorFilePath"];
            _professorRepository = new ProfessorRepository(_professorFilePath);

            _studentFilePath = configuration["FileAddresses:StudentFilePath"];
            _studentRepository = new StudentRepository(_studentFilePath);
        }

        public Person? Login(LoginViewModel login)
        {
            List<Student> students = _studentRepository.GetAll();
            List<Professor> professors = _professorRepository.GetAll();
            Person currentUser = students.FirstOrDefault(p => p.Email == login.Email && p.Password == login.Password);

            if (currentUser == null) 
                currentUser = professors.FirstOrDefault(p => p.Email == login.Email && p.Password == login.Password);

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
                if (register.RoleId == "2")
                {
                    Student student = new Student()
                    {
                        Id = _studentRepository.GetAll().Count() + 1,
                        UserName = register.UserName,
                        Password = register.Password,
                        Email = register.Email,
                        role = new PersonRole()
                        {
                            Id = 2,
                            Title = "Student"
                        }
                    };

                    _studentRepository.Create(student);
                    _studentRepository.SaveChanges();
                }
                else if (register.RoleId == "1")
                {
                    Professor professor = new Professor()
                    {
                        Id = _professorRepository.GetAll().Count() + 1,
                        UserName = register.UserName,
                        Password = register.Password,
                        Email = register.Email,
                        role = new PersonRole()
                        {
                            Id = 1,
                            Title = "Professor"
                        }
                    };

                    _professorRepository.Create(professor);
                    _professorRepository.SaveChanges();
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
