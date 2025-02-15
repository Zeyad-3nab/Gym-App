using Gym.Api.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IExerciseSystemRepository
    {
        Task<IEnumerable<ExerciseSystem>> GetAllAsync();
        Task<ExerciseSystem> GetById(int Id);
        Task<int> AddExerciseSystem(ExerciseSystem exerciseSystem);
        Task<int> RemoveExerciseSystem(ExerciseSystem exerciseSystem);
        Task<int> AddExerciseSystemItem(int ExerciseSystemId, ExerciseSystemItem exerciseSystemItem);
        Task<int> RemoveExerciseSystemItem(int ExerciseSystemId , ExerciseSystemItem exerciseSystemItem);
    }
}
