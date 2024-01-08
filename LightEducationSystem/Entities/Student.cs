using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Entities
{
    public class Student : Person
    {
        public List<int> TrainingCourseStudentCardsId { get; set; }  = new List<int>();
    }
}
