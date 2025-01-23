using Gym.Api.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // OnConfiguring بدل ما اعمل الميثود بتاعت ال   
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<ExerciseSystem> ExerciseSystems { get; set; }
        public DbSet<FoodSystem> FoodSystems { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<TrainerData> TrainersData { get; set; }
    }
}
