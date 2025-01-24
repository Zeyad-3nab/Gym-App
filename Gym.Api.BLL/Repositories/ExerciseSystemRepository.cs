using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Data.Contexts;
using Gym.Api.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Repositories
{
    public class ExerciseSystemRepository : IExerciseSystemRepository
    {

        private readonly ApplicationDbContext context;

        public ExerciseSystemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<ExerciseSystem>> GetAllAsync()
        {
            return await context.ExerciseSystems.ToListAsync();
        }

        public async Task<ExerciseSystem> GetByIdAsync(int Id)
        {
            return await context.ExerciseSystems.FindAsync(Id);
        }

        public async Task AddAsync(ExerciseSystem entity)
        {
            await context.ExerciseSystems.AddAsync(entity);
            context.SaveChanges();
        }

        public void Delete(ExerciseSystem entity)
        {
            context.ExerciseSystems.Remove(entity);
            context.SaveChanges();
        }

        public void Update(ExerciseSystem entity)
        {
            context.Set<ExerciseSystem>().Update(entity);
            context.SaveChanges();
        }

        public async Task<IEnumerable<ExerciseSystem>> SearchByName(string Name)
        {
            return await context.ExerciseSystems.Where(E => E.Name.Contains(Name)).ToListAsync();
        }
    }
}
