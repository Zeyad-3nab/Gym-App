﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class ExerciseSystem:BaseModel<int>
    {
        public string Name { get; set; }
        public string TargetMuscle { get; set; }

    }
}
