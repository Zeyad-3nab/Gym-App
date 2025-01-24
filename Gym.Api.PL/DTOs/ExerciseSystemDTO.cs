using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class ExerciseSystemDTO
    {


        [Required(ErrorMessage ="Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "TargetMuscle is required")]
        public string TargetMuscle { get; set; }
    }
}
