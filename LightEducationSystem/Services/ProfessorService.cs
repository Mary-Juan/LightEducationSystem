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

        private readonly string _trainingCourseStudentCardFilePath;
        private readonly IGenericRepository<TrainingCourseStudentCard> _trainingCourseStudentCardRepository;

        private readonly IProfessorRepository _professorRepository;
        private readonly string _professorFilePath;

        private readonly IStudentRepository _studentRepository;
        private readonly string _studentFilePath;

        public ProfessorService(IConfiguration configuration)
        {
            _trainingCourseFilePath = configuration["FileAddresses:TrainingCourseFilePath"];
            _trainingCourseRepository = new GenericRepository<TrainingCourse>(_trainingCourseFilePath);

            _trainingCourseStudentCardFilePath = configuration["FileAddresses:TrainingCourseStudentCardFilePath"];
            _trainingCourseStudentCardRepository = new GenericRepository<TrainingCourseStudentCard>(_trainingCourseStudentCardFilePath);

            _professorFilePath = configuration["FileAddresses:ProfessorFilePath"];
            _professorRepository = new ProfessorRepository(_professorFilePath);

            _studentFilePath = configuration["FileAddresses:StudentFilePath"];
            _studentRepository = new StudentRepository(_studentFilePath);


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
                Professor professor = _professorRepository.GetByID(professorId);
                professor.TrainingCoursesId.Add(newTrainingCourse.Id);
                _professorRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Student> GetStudentsOfTrainingCourse(int trainingCourseId)
        {
            List<Student> students = new List<Student>();
            TrainingCourse trainingCourse = _trainingCourseRepository.GetByID(trainingCourseId);

            foreach(var cardId in trainingCourse.TrainingCourseStudentCardsId)
            {
                var card = _trainingCourseStudentCardRepository.GetByID(cardId);
                var student = _studentRepository.GetByID(card.StudentId);
                students.Add(student);
            }

            return students;
        }

        public bool RateStudent(int studentId, int score)
        {
            try
            {
                var card = _trainingCourseStudentCardRepository.GetAll().FirstOrDefault(c => c.StudentId == studentId);
                card.Score = score;
                _trainingCourseStudentCardRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
