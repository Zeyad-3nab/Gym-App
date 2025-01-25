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
    public class FoodsConfigurations : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasMany(e => e.applicationUsers)
            .WithMany()
            .UsingEntity<ApplicationUserFood>();
            builder.Property(P => P.Id).UseIdentityColumn(1, 1);
            builder.Property(F => F.Name).IsRequired();
            builder.Property(F => F.ImageURL).IsRequired();
            builder.Property(F => F.NumOfProtein).IsRequired();
            builder.Property(F => F.NumOfCalories).IsRequired();
        }
    }
}
