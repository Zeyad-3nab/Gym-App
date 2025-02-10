using Gym.Api.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.DAL.Data.Configurations
{
    public class ExerciseSystemItemConfigurations : IEntityTypeConfiguration<ExerciseSystemItem>
    {
        public void Configure(EntityTypeBuilder<ExerciseSystemItem> builder)
        {

            builder.HasOne(es => es.exercise)
           .WithMany()
           .HasForeignKey(es => es.ExerciseId);

            builder.Property(es => es.ExerciseId).IsRequired();
            builder.Property(es => es.NumOfGroups).IsRequired();
            builder.Property(es => es.NumOfCount).IsRequired();

        }
    }
}
