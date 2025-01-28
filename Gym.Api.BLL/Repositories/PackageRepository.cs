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
    public class PackageRepository:IPackageRepository
    {
        private readonly ApplicationDbContext context;

        public PackageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            return await context.Packages.ToListAsync();
        }

        public async Task<Package> GetByIdAsync(int Id)
        {
            return await context.Packages.FindAsync(Id);
        }

        public async Task<int> AddAsync(Package entity)
        {
            await context.Packages.AddAsync(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(Package entity)
        {
            context.Packages.Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(Package entity)
        {
            context.Packages.Update(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Package>> SearchByName(string Name)
        {
            return await context.Packages.Where(E => E.Name.Contains(Name)).ToListAsync();
        }
    }
}
