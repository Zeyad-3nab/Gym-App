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
    public class PackageConfigurations : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.Property(P => P.Id).UseIdentityColumn(1, 1);
            builder.Property(P => P.OldPrice).IsRequired();
            builder.Property(P => P.NewPrice).IsRequired();
            builder.Property(P => P.Name).IsRequired();
            builder.Property(P => P.PackageType).IsRequired();
            builder.Property(P => P.Duration).IsRequired();
            builder.Property(P => P.IsActive).IsRequired();
        }
    }
}