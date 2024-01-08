using LightEducationSystem.DataAccess.Repositories;
using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.ViewModels;

namespace LightEducationSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly string _trainingCourseFilePath;
        private readonly IGenericRepository<TrainingCourse> _trainingCourseRepository;

        private readonly string _trainingCourseStudentCardFilePath;
        private readonly IGenericRepository<TrainingCourseStudentCard> _trainingCourseStudentCardRepository;

        private readonly IProfessorRepository _professorRepository;
        private readonly string _professorFilePath;

        private readonly IStudentRepository _studentRepository;
        private readonly string _studentFilePath;

        public StudentService(IConfiguration configuration)
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

        public List<TrainingCourseViewModel> GetAllTrainingCourses()
        {
            return _trainingCourseRepository.GetAll().Select(x => new TrainingCourseViewModel
            {
                Capacity = x.Capacity,
                ProfessorId = x.ProfessorId,
                Title = x.Title,
                Time = x.Time
            }).ToList();
        }

        public ProfessorViewModel GetProfessorDetail(int professionId)
        {
            var professor = _professorRepository.GetByID(professionId);
            List<TrainingCourseViewModel> trainingCourses = _trainingCourseRepository.GetAll().Where(t => professor.TrainingCoursesId.Contains(t.Id)).Select(t => new TrainingCourseViewModel
            {
                ProfessorId=professionId,
                Capacity = t.Capacity,
                Time = t.Time,
                Title = t.Title
            }).ToList();
            
            return new ProfessorViewModel()
            {
                Email = professor.Email,
                UserName = professor.UserName,
                TrainingCourses = trainingCourses,
            };
        }

        public List<TrainingCourseViewModel> GetProfessorTrainingCourse(int professionId)
        {
            var professor = GetProfessorDetail(professionId);
            return professor.TrainingCourses;
        }

        public List<StudentCardViewModel> GetStudentTrainingCourse(int studentId)
        {
            var student = _studentRepository.GetByID(studentId);

            return _trainingCourseStudentCardRepository.GetAll().Where(tc => student.TrainingCourseStudentCardsId.Contains(tc.Id)).Select(tc => new StudentCardViewModel
            {
                StudentId = studentId,
                Score = tc.Score,
                TrainingCourseId = tc.TrainingCourseId,
                StudentName = student.UserName
            }).ToList();
        }

        public TrainingCourseViewModel GetTrainingCourseDetail(int trainingCourseId)
        {
            var trainingCourse = _trainingCourseRepository.GetByID(trainingCourseId);

            return new TrainingCourseViewModel()
            {
                Time = trainingCourse.Time,
                Capacity = trainingCourse.Capacity,
                ProfessorId = trainingCourse.ProfessorId,
                Title = trainingCourse.Title
            };
        }

        public bool RegisterTrainingCourse(int studentId, int trainingCourseId)
        {
            try
            {
                var trainingCourseStudentCard = new TrainingCourseStudentCard()
                {
                    StudentId = studentId,
                    Id = _trainingCourseStudentCardRepository.GetAll().Count + 1,
                    TrainingCourseId = trainingCourseId,
                    Score = 0
                };

                _trainingCourseStudentCardRepository.Create(trainingCourseStudentCard);
                _trainingCourseStudentCardRepository.SaveChanges();

                var student = _studentRepository.GetByID(studentId);
                student.TrainingCourseStudentCardsId.Add(trainingCourseStudentCard.Id);
                _studentRepository.SaveChanges();

                var trainingCourse = _trainingCourseRepository.GetByID(trainingCourseId);
                trainingCourse.TrainingCourseStudentCardsId.Add(trainingCourseStudentCard.Id);
                _trainingCourseRepository.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
