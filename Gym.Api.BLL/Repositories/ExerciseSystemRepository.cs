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
    public class ExerciseSystemRepository : IExerciseSystemRepository
    {
        private readonly ApplicationDbContext _context;

        public ExerciseSystemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddExerciseSystemItem( int ExerciseSystemId, ExerciseSystemItem exerciseSystemItem)
        {
            var ExerciseSystem = await _context.exerciseSystems.FirstOrDefaultAsync(e=>e.Id==ExerciseSystemId);
            if (ExerciseSystem is not null) 
            {
                ExerciseSystem.exerciseSystemsItems.Add(exerciseSystemItem);
                return await _context.SaveChangesAsync();
            }
            return 1;
        }

        public async Task<int> AddExerciseSystem(ExerciseSystem exerciseSystem)
        {
             await _context.exerciseSystems.AddAsync(exerciseSystem);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExerciseSystem>> GetAllAsync()
        {
            return await _context.exerciseSystems.Include(e=>e.exerciseSystemsItems).ToListAsync();
        }

        public async Task<ExerciseSystem> GetById(int Id)
        {
            return await _context.exerciseSystems.Include(e => e.exerciseSystemsItems).FirstOrDefaultAsync(e=>e.Id==Id);
        }

        public async Task<int> RemoveExerciseSystemItem(int ExerciseSystemId, ExerciseSystemItem exerciseSystemItem)
        {
            var ExerciseSystem = await _context.exerciseSystems.FirstOrDefaultAsync(e => e.Id == ExerciseSystemId);
            if (ExerciseSystem is not null)
            {
                ExerciseSystem.exerciseSystemsItems.Remove(exerciseSystemItem);
                return await _context.SaveChangesAsync();
            }
            return 1;
        }

        public async Task<int> RemoveExerciseSystem(ExerciseSystem exerciseSystem)
        {
            _context.exerciseSystems.Remove(exerciseSystem);
            return await _context.SaveChangesAsync();
        }
    }
}