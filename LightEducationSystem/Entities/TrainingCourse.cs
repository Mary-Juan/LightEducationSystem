using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Entities
{
    public class TrainingCourse : BaseEntity
    {
        public string Title { get; set; }
        public int Capacity { get; set; }
        public int ProfessorId { get; set; }
        public List<int> TrainingCourseStudentCardsId { get; set; } = new List<int>();
        public TimeSpan Time { get; set; }
        public string? ImageAddress { get; set; }
        public string Detail { get; set; }
    }
}
