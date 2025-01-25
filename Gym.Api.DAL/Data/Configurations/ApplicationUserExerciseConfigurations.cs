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
    public class ApplicationUserExerciseConfigurations : IEntityTypeConfiguration<ApplicationUserExercise>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserExercise> builder)
        {
            builder.Property(E => E.NumOfGroups).IsRequired();
            builder.Property(E => E.NumOfCount).IsRequired();
        }
    }
}
