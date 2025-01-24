using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Models
{
    public class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
