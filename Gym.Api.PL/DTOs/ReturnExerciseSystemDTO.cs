using Gym.Api.DAL.Models;

namespace Gym.Api.PL.DTOs
{
    public class ReturnExerciseSystemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ExerciseSystemItem> exerciseSystemsItems { get; set; }
    }
}
