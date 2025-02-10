using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class Food:BaseModel<int>
    {
        public string Name { get; set; }
        public double NumOfCalories { get; set; }
        public double NumOfProtein { get; set; }
        public string ImageURL { get; set; }

        //public List<ApplicationUser>? applicationUsers { get; set; }
    }
}