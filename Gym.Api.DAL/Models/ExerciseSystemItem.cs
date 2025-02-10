using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class ExerciseSystemItem:BaseModel<int>
    {
        public int ExerciseId { get; set; }
        public int NumOfGroups { get; set; }
        public int NumOfCount { get; set; }
        public Exercise exercise { get; set; }
    }
}