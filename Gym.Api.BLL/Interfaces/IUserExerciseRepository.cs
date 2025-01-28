using Gym.Api.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IUserExerciseRepository
    {
        Task<int> AddExerciseToUser(ApplicationUserExercise userExercise);
        Task<int> RemoveExerciseFromUser(string userId, int ExerciseId);
        Task<IEnumerable<ApplicationUserExercise>> GetAllExercisesOfUser(string userId);
    }
}
