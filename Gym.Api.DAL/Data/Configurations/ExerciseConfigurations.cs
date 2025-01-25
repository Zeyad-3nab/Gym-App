using Gym.Api.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Api.DAL.Data.Configurations
{
    public class ExerciseConfigurations : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasMany(e => e.applicationUsers)
                .WithMany()
                .UsingEntity<ApplicationUserExercise>();

            builder.Property(P => P.Id).UseIdentityColumn(1, 1);
            builder.Property(E => E.Name).IsRequired();
            builder.Property(E => E.TargetMuscle).IsRequired();
            builder.Property(E => E.VideoLink).IsRequired(false);
            builder.Property(E => E.Comment).IsRequired(false);
        }
    }
}
