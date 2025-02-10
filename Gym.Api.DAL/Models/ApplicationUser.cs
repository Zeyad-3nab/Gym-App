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
        public string Gender { get; set; }
        public DateTime StartPackage { get; set; }
        public DateTime EndPackage { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }

        public List<Food>? foods { get; set; }
        public List<Exercise>? exercises { get; set; }



    }
}
