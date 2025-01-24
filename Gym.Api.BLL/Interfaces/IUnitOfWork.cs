using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IFoodSystemRepository  foodSystemRepository { get; }
        public IExerciseSystemRepository  exerciseSystemRepository { get; }
        public IPackageRepository  packageRepository { get; }
        public ITrainerDataRepository  trainerDataRepository { get; }

    }
}
