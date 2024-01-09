using LightEducationSystem.Entities;
using LightEducationSystem.ViewModels;

namespace LightEducationSystem.Services.Interfaces
{
    public interface IStudentService
    {
        public List<TrainingCourseViewModel> GetAllTrainingCourses();
        public bool RegisterTrainingCourse(int studentId, int trainingCourseId);
        public List<StudentCardViewModel> GetStudentTrainingCourse(int studentId);
        public List<TrainingCourseViewModel> GetProfessorTrainingCourse(int professorId);
        public TrainingCourseViewModel GetTrainingCourseDetail(int trainingCourseId);
        public ProfessorViewModel GetProfessorDetail(int professorId);
        public StudentViewModel GetStudentDetails(int studentId);
    }
}
