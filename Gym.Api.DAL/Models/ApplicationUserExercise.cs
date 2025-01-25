using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class ApplicationUserExercise
    {
        public string applicationUserId { get; set; }
        public int exerciseId { get; set; }
        public int NumOfGroups { get; set; }
        public int NumOfCount { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public Exercise exercise { get; set; }
    }
}
