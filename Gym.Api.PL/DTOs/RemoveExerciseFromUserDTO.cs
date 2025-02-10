using System.ComponentModel.DataAnnotations;

namespace Gym.Api.PL.DTOs
{
    public class RemoveExerciseFromUserDTO
    {
        [Required(ErrorMessage = "ExerciseIdRequired")]
        public int ExerciseId { get; set; }

        [Required(ErrorMessage ="UserIdRequired")]
        public string UserId { get; set; }
    }
}
