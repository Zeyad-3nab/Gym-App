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

        public async Task AddAsync(Package entity)
        {
            await context.Packages.AddAsync(entity);
            context.SaveChanges();
        }

        public void Delete(Package entity)
        {
            context.Packages.Remove(entity);
            context.SaveChanges();
        }

        public void Update(Package entity)
        {
            context.Packages.Update(entity);
            context.SaveChanges();
        }

        public async Task<IEnumerable<Package>> SearchByName(string Name)
        {
            return await context.Packages.Where(E => E.Name.Contains(Name)).ToListAsync();
        }
    }
}
