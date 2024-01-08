using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Entities
{
    public class Student : Person
    {
        public List<TrainingCourseStudentCard> StudentCards { get; set; }  = new List<TrainingCourseStudentCard>();
    }
}
