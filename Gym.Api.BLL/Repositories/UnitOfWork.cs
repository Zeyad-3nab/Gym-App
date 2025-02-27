﻿using Gym.Api.BLL.Interfaces;
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
        private ITrainerDataRepository _trainerDataRepository;
        private IPackageRepository _packageRepository;
        private IExerciseRepository _exerciseRepository;
        private IFoodRepository _foodRepository;
        private IUserExerciseRepository _userExerciseRepository;
        private IUserFoodRepository _userFoodRepository;
        private IExerciseSystemItemRepository _exerciseSystemItemRepository;
        private IExerciseSystemRepository _exerciseSystemRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            _trainerDataRepository=new TrainerDataRepository(context);
            _packageRepository=new PackageRepository(context);
            _foodRepository=new FoodRepository(context);
            _exerciseRepository=new ExerciseRepository(context);
            _userExerciseRepository = new UserExerciseRepository(context);
            _userFoodRepository = new UserFoodRepository(context);
            _exerciseSystemItemRepository = new ExerciseSystemItemRepository(context);
            _exerciseSystemRepository = new ExerciseSystemRepository(context);
        }


        public IPackageRepository packageRepository => _packageRepository;

        public ITrainerDataRepository trainerDataRepository => _trainerDataRepository;

        public IFoodRepository foodRepository => _foodRepository;

        public IExerciseRepository exerciseRepository => _exerciseRepository;

        public IUserExerciseRepository userExerciseRepository => _userExerciseRepository;

        public IUserFoodRepository userFoodRepository => _userFoodRepository;

        public IExerciseSystemItemRepository exerciseSystemItemRepository => _exerciseSystemItemRepository;

        public IExerciseSystemRepository exerciseSystemRepository => _exerciseSystemRepository;
    }
}