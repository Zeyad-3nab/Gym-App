using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IFoodSystemRepository _foodSystemRepository;
        private IExerciseSystemRepository _exerciseSystemRepository;
        private ITrainerDataRepository _trainerDataRepository;
        private IPackageRepository _packageRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            _foodSystemRepository=new FoodSystemRepository(context);
            _exerciseSystemRepository=new ExerciseSystemRepository(context);
            _trainerDataRepository=new TrainerDataRepository(context);
            _packageRepository=new PackageRepository(context);
        }

        public IFoodSystemRepository foodSystemRepository => _foodSystemRepository;

        public IExerciseSystemRepository exerciseSystemRepository => _exerciseSystemRepository;

        public IPackageRepository packageRepository => _packageRepository;

        public ITrainerDataRepository trainerDataRepository => _trainerDataRepository;
    }
}
