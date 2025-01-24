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
    public class ExerciseSystemConfigurations : IEntityTypeConfiguration<ExerciseSystem>
    {
        public void Configure(EntityTypeBuilder<ExerciseSystem> builder)
        {
            builder.Property(E => E.Id).UseIdentityColumn(1, 1);
            builder.Property(E => E.Name).IsRequired();
            builder.Property(E => E.TargetMuscle).IsRequired();

        }
    }
}
