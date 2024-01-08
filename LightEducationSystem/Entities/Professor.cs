using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Entities
{
    public class Professor : Person
    {
        public List<int> TrainingCoursesId { get; set; } = new List<int>();

    }
}
