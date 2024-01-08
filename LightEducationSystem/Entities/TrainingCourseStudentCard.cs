using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Entities
{
    public class TrainingCourseStudentCard : BaseEntity
    {
        public int StudentId { get; set; }
        public int TrainingCourseId { get; set; }
        public int Score { get; set; }
    }
}
