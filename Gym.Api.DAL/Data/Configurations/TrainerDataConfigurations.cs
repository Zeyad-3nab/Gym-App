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
    public class TrainerDataConfigurations : IEntityTypeConfiguration<TrainerData>
    {
        public void Configure(EntityTypeBuilder<TrainerData> builder)
        {

            builder.HasOne(e => e.Package)
               .WithMany()
               .HasForeignKey(e => e.PackageId);

            builder.Property(T => T.Id).UseIdentityColumn(1, 1);
            builder.Property(T => T.UserName).IsRequired();
            builder.Property(T => T.Age).IsRequired();
            builder.Property(T => T.Address).IsRequired();
            builder.Property(T => T.Phone).IsRequired();
            builder.Property(T => T.Email).IsRequired();
            builder.Property(T => T.Long).IsRequired();
            builder.Property(T => T.Weight).IsRequired();
            builder.Property(T => T.DailyWork).IsRequired();
            builder.Property(T => T.AreYouSomker).IsRequired();
            builder.Property(T => T.AimOfJoin).IsRequired();
            builder.Property(T => T.AnyPains).IsRequired();
            builder.Property(T => T.AllergyOfFood).IsRequired();
            builder.Property(T => T.FoodSystem).IsRequired();
            builder.Property(T => T.Gender).IsRequired();
            builder.Property(T => T.NumberOfMeals).IsRequired();
            builder.Property(T => T.LastExercise).IsRequired();
            builder.Property(T => T.AnyInfection).IsRequired();
            builder.Property(T => T.AbilityOfSystemMoney).IsRequired();
            builder.Property(T => T.NumberOfDayes).IsRequired();
            builder.Property(T => T.PackageId).IsRequired();
        }
    }
}
