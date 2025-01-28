using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Data.Contexts;
using Gym.Api.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Repositories
{
    public class UserExerciseRepository : IUserExerciseRepository
    {
        private readonly ApplicationDbContext _Context;

        public UserExerciseRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<int> AddExerciseToUser(ApplicationUserExercise userExercise)
        {
            _Context.applicationUserExercises.Add(userExercise);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> RemoveExerciseFromUser(string userId, int ExerciseId)
        {
            var userExercise = _Context.applicationUserExercises.FirstOrDefault(e => e.applicationUserId == userId && e.exerciseId == ExerciseId);
            if (userExercise != null)
            {
                _Context.applicationUserExercises.Remove(userExercise);
                return await _Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ApplicationUserExercise>> GetAllExercisesOfUser(string userId)
        {
            return await _Context.applicationUserExercises.Where(e=>e.applicationUserId==userId).ToListAsync();
        }
    }
}
