using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Model.Entities
{
    public class TrainingCourseStudentCard 
    {
        public int StudentId { get; set; }
        public int TrainingCourseId { get; set; }
        public int Score { get; set; }
    }
}
