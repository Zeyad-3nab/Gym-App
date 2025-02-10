using Gym.Api.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            modelBuilder.Entity<ApplicationUserFood>().HasKey(table => new
            {
                table.foodId,
                table.applicationUserId
            });

            modelBuilder.Entity<ApplicationUserExercise>().HasKey(table => new
            {
                table.exerciseId,
                table.applicationUserId
            });

        }


        public DbSet<Package> Packages { get; set; }
        public DbSet<TrainerData> TrainersData { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<ApplicationUserFood> applicationUserFoods { set; get; }
        public DbSet<ApplicationUserExercise> applicationUserExercises { set; get; }
        public DbSet<ExerciseSystem> exerciseSystems { set; get; }
        public DbSet<ExerciseSystemItem> exerciseSystemItems { set; get; }
    }
}
