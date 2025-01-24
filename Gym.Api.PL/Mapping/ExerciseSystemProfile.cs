using AutoMapper;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;

namespace Gym.Api.PL.Mapping
{
    public class ExerciseSystemProfile:Profile
    {
        public ExerciseSystemProfile()
        {
            CreateMap<ExerciseSystem, ExerciseSystemDTO>().ReverseMap();
        }
    }
}
