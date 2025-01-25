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

            builder.HasOne(e => e.Package)
               .WithMany()
               .HasForeignKey(e => e.PackageId);

            builder.HasMany(e => e.foods)
                .WithMany()
                .UsingEntity<ApplicationUserFood>();

            builder.HasMany(e => e.exercises)
                .WithMany()
                .UsingEntity<ApplicationUserExercise>();

            builder.Property(A => A.Address).IsRequired();
            builder.Property(A => A.Long).IsRequired();
            builder.Property(A => A.Weight).IsRequired();
            builder.Property(A => A.PackageId).IsRequired();
        }
    }
}
