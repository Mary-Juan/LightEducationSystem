﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEducationSystem.Entities
{
    internal class Professor : Person
    {
        public ICollection<TrainingCourse> Courses { get; set; }

    }
}
