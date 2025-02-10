using Gym.Api.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IExerciseSystemItemRepository
    {
        Task<IEnumerable<ExerciseSystemItem>> GetAllAsync();
        Task<ExerciseSystemItem> GetByIdAsync(int Id);
        Task<int> AddAsync(ExerciseSystemItem entity);
        Task<int> Update(ExerciseSystemItem entity);
        Task<int> Delete(ExerciseSystemItem entity);
    }
}