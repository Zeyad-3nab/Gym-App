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
            builder.Property(T => T.Id).UseIdentityColumn(1, 1);

            builder.HasOne(e => e.Package)
               .WithMany()
               .HasForeignKey(e => e.PackageId);
        }
    }
}
