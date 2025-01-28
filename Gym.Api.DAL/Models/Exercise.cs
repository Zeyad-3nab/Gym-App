using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class Exercise:BaseModel<int>
    {
        public string Name { get; set; }
        public string VideoLink { get; set; }
        public string Comment { get; set; }
        public string TargetMuscle { get; set; }
        //public List<ApplicationUser>? applicationUsers { get; set; }

    }
}
