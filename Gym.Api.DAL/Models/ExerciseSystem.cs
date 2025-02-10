using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class ExerciseSystem:BaseModel<int>
    {
        public string Name { get; set; }
        public IEnumerable<ExerciseSystem> exerciseSystems { get; set; }
    }
}