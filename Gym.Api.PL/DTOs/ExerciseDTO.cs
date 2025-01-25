using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class ExerciseDTO
    {

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        
        public string? VideoLink { get; set; }
        public string? Comment { get; set; }

        [Required(ErrorMessage = "TargetMuscle is required")]
        public string TargetMuscle { get; set; }
    }
}
