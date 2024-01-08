using LightEducationSystem.Entities;

namespace LightEducationSystem.ViewModels
{
    public class StudentCardViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int TrainingCourseId { get; set; }
        public int Score { get; set; }
    }
}
