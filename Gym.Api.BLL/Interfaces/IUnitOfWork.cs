using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IPackageRepository  packageRepository { get; }
        public IExerciseSystemRepository  exerciseSystemRepository { get; }
        public ITrainerDataRepository  trainerDataRepository { get; }
        public IExerciseRepository  exerciseRepository { get; }
        public IFoodRepository  foodRepository { get; }
        public IUserExerciseRepository  userExerciseRepository { get; }
        public IUserFoodRepository  userFoodRepository { get; }
        public IExerciseSystemItemRepository  exerciseSystemItemRepository { get; }


    }
}
