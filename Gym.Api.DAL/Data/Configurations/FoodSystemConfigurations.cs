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
    public class FoodSystemConfigurations : IEntityTypeConfiguration<FoodSystem>
    {
        public void Configure(EntityTypeBuilder<FoodSystem> builder)
        {
            builder.Property(F => F.Id).UseIdentityColumn(1, 1);
            builder.Property(F => F.Name).IsRequired();
            builder.Property(F => F.NumberOfProteins).IsRequired();
            builder.Property(F => F.NumberOfCalories).IsRequired();
        }
    }
}
