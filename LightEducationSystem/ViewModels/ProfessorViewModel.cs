namespace LightEducationSystem.ViewModels
{
    public class ProfessorViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
       public List<TrainingCourseViewModel> TrainingCourses { get; set; } = new List<TrainingCourseViewModel>();
    }
}
