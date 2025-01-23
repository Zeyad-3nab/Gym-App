using Gym.Api.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Data.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(e => e.ExerciseSystem)
               .WithMany()
               .HasForeignKey(e => e.ExerciseSystemId);


            builder.HasOne(e => e.FoodSystem)
               .WithMany()
               .HasForeignKey(e => e.FoodSystemId);


            builder.HasOne(e => e.Package)
               .WithMany()
               .HasForeignKey(e => e.PackageId);
        }
    }
}
