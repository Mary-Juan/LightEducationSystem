using LightEducationSystem.Entities;
using LightEducationSystem.ViewModels;

namespace LightEducationSystem.Services.Interfaces
{
    public interface IProfessorService
    {
        public bool AddTrainingCourse(TrainingCourseViewModel trainingCourse, int professorId);
        public List<StudentViewModel> GetStudentsOfTrainingCourse(int trainingCourseId);
        public bool RateStudent(StudentCardViewModel inputCard);
        public ProfessorViewModel GetProfessorDetail(int professorId);
    }
}
