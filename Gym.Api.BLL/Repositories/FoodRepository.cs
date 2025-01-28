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
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext context;

        public FoodRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            return await context.Food.ToListAsync();
        }

        public async Task<Food> GetByIdAsync(int Id)
        {
            return await context.Food.FindAsync(Id);
        }

        public async Task<int> AddAsync(Food entity)
        {
            await context.Food.AddAsync(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(Food entity)
        {
            context.Food.Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(Food entity)
        {
            context.Food.Update(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Food>> SearchByName(string Name)
        {
            return await context.Food.Where(E => E.Name.Contains(Name)).ToListAsync();
        }
    }
}
