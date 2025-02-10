using Gym.Api.DAL.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class ExerciseDTO
    {

        [Required(ErrorMessage = "IdRequired")]
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        
        public string? VideoLink { get; set; }
        public string? Comment { get; set; }

        [Required(ErrorMessage = "TargetMuscleRequired")]
        public string TargetMuscle { get; set; }
    }
}
