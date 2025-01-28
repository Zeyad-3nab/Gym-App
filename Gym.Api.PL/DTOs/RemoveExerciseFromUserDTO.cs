using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class RemoveExerciseFromUserDTO
    {
        [Required(ErrorMessage ="Exercise Id is required")]
        public int ExerciseId { get; set; }
        [Required(ErrorMessage ="User Id is required")]
        public string UserId { get; set; }
    }
}
