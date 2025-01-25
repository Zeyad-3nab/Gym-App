using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class ApplicationUserFood
    {
        public string applicationUserId { get; set; }
        public int foodId { get; set; }
        public int NumOfGrams { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public Food food { get; set; }
    }
}
