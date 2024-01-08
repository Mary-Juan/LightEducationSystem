namespace LightEducationSystem.ViewModels
{
    public class StudentViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<StudentCardViewModel> StudentCards { get; set; } = new List<StudentCardViewModel>();
    }
}
