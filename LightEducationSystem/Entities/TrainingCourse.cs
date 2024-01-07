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
        public string ProfessorId { get; set; }
        public ICollection<TrainingCourseStudentCard> Cards { get; set; }
        public TimeSpan Time { get; set; }
    }
}
