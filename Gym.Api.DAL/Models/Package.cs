using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public TypeOfPackage Type { get; set; }
    }
}
