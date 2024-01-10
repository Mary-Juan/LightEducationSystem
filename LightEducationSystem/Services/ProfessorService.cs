using LightEducationSystem.DataAccess.Repositories;
using LightEducationSystem.DataAccess.Repositories.Interfaces;
using LightEducationSystem.Entities;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.ViewModels;
using System.IO;

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
                ProfessorId = professorId
            };

            try
            {
                var imageExtention = Path.GetExtension(trainingCourse.Image.FileName).Trim();
                string imageFilePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","imgs", newTrainingCourse.Id + imageExtention);
                newTrainingCourse.ImageAddress = imageFilePath;

                using (var stream = new FileStream(imageFilePath, FileMode.Create))
                {
                    trainingCourse.Image.CopyTo(stream);
                }

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

        public List<StudentViewModel> GetStudentsOfTrainingCourse(int trainingCourseId)
        {
            List<StudentViewModel> students = new List<StudentViewModel>();
            TrainingCourse trainingCourse = _trainingCourseRepository.GetByID(trainingCourseId);

            foreach(var cardId in trainingCourse.TrainingCourseStudentCardsId)
            {
                var card = _trainingCourseStudentCardRepository.GetByID(cardId);
                var student = _studentRepository.GetByID(card.StudentId);
                students.Add(new StudentViewModel()
                {
                    Email = student.Email,
                    UserName = student.UserName,
                    StudentCards = _trainingCourseStudentCardRepository.GetAll().Where(c => student.TrainingCourseStudentCardsId.Contains(c.Id)).Select(c => new StudentCardViewModel
                    {
                        Score = c.Score,
                        StudentId = student.Id,
                        StudentName = student.UserName,
                        TrainingCourseId = c.Id
                    }).ToList(),
                });
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

        public ProfessorViewModel GetProfessorDetail(int professorId)
        {
            var professor = _professorRepository.GetByID(professorId);
            List<TrainingCourseViewModel> trainingCourses = _trainingCourseRepository.GetAll().Where(t => professor.TrainingCoursesId.Contains(t.Id)).Select(t => new TrainingCourseViewModel
            {
                Id = t.Id,
                ProfessorId = professorId,
                Capacity = t.Capacity,
                Time = t.Time,
                Title = t.Title,
                ImageAddress = t.ImageAddress,
                RemainingCapacity = t.Capacity - t.TrainingCourseStudentCardsId.Count()
            }).ToList();

            return new ProfessorViewModel()
            {
                Email = professor.Email,
                UserName = professor.UserName,
                TrainingCourses = trainingCourses,
            };
        }
    }
}
