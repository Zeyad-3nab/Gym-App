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
    public class ExerciseSystemItemRepository : IExerciseSystemItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ExerciseSystemItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(ExerciseSystemItem entity)
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ExerciseSystemItem entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(ExerciseSystemItem entity)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExerciseSystemItem>> GetAllAsync()
            => await _context.exerciseSystemItems.Include(e=>e.exercise).ToListAsync();

        public async Task<ExerciseSystemItem> GetByIdAsync(int Id)
             => await _context.exerciseSystemItems.Include(e => e.exercise).FirstOrDefaultAsync(e => e.Id == Id);
    }
}