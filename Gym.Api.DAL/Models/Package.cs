using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class Package:BaseModel<int>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; } = true;
        public TypeOfPackage Type { get; set; }
    }
}
