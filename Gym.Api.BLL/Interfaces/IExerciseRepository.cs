using Gym.Api.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IExerciseRepository:IGenaricRepository<Exercise  ,int>
    {
        public Task<IEnumerable<Exercise>> SearchByTargetMuscle(string TargetMuscle);
    }
}
