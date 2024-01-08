using LightEducationSystem.DataAccess.Repositories;
using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.ViewModels;

namespace LightEducationSystem.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly string _trainingCourseFilePath;
        private readonly IGenericRepository<TrainingCourse> _trainingCourseRepository;
        private readonly string _peopleFilePath;
        private readonly IGenericRepository<Person> _personRepository;

        public ProfessorService(IConfiguration configuration)
        {
            _trainingCourseFilePath = configuration["FileAddresses:TrainingCourseFilePath"];
            _trainingCourseRepository = new GenericRepository<TrainingCourse>(_trainingCourseFilePath);
            _peopleFilePath = configuration["FileAddresses:PeopleFilePath"];
            _personRepository = new GenericRepository<Person>(_peopleFilePath);
        }

        public bool AddTrainingCourse(TrainingCourseViewModel trainingCourse, int professorId)
        {
            TrainingCourse newTrainingCourse = new TrainingCourse()
            {
                Id = _trainingCourseRepository.GetAll().Count + 1,
                Title = trainingCourse.Title,
                Time = trainingCourse.Time,
                Capacity = trainingCourse.Capacity,
                ProfessorId = professorId,
            };

            try
            {
                _trainingCourseRepository.Create(newTrainingCourse);
                _trainingCourseRepository.SaveChanges();
                Person person = _personRepository.GetByID(professorId);
                person.
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Student> GetStudentsOfTrainingCourse(int trainingCourseId)
        {
            TrainingCourse trainingCourse = _trainingCourseRepository.GetByID(trainingCourseId);

            foreach
        }

        public bool RateStudent(int dtudentId)
        {
            throw new NotImplementedException();
        }
    }
}
