using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class FoodSystem:BaseModel<int>
    {
        public string Name { get; set; }
        public double NumberOfCalories { get; set; }
        public double NumberOfProteins { get; set; }
    }
}

