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
    public class ApplicationUserFoodConfigurations : IEntityTypeConfiguration<ApplicationUserFood>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserFood> builder)
        {
            builder.Property(E => E.NumOfGrams).IsRequired();
        }
    }
}
