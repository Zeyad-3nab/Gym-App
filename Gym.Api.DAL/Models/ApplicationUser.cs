using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; } 
        public double Long { get; set; }
        public double Weight { get; set; }
        public double Age { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }

        public int FoodSystemId { get; set; }
        public FoodSystem FoodSystem { get; set; }

        public int ExerciseSystemId { get; set; }
        public ExerciseSystem  ExerciseSystem { get; set; }


    }
}
