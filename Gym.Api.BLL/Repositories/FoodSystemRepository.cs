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
    public class FoodSystemRepository:IFoodSystemRepository
    {
        private readonly ApplicationDbContext context;

        public FoodSystemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<FoodSystem>> GetAllAsync()
        {
            return await context.FoodSystems.ToListAsync();
        }

        public async Task<FoodSystem> GetByIdAsync(int Id)
        {
            return await context.FoodSystems.FindAsync(Id);
        }

        public async Task AddAsync(FoodSystem entity)
        {
            await context.FoodSystems.AddAsync(entity);
            context.SaveChanges();
        }

        public void Delete(FoodSystem entity)
        {
            context.FoodSystems.Remove(entity);
            context.SaveChanges();
        }

        public void Update(FoodSystem entity)
        {
            context.FoodSystems.Update(entity);
            context.SaveChanges();
        }

        public async Task<IEnumerable<FoodSystem>> SearchByName(string Name)
        {
            return await context.FoodSystems.Where(E => E.Name.Contains(Name)).ToListAsync();
        }
    }
}
