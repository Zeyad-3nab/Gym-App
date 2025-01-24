using AutoMapper;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;

namespace Gym.Api.PL.Mapping
{
    public class FoodSystemProfile:Profile
    {
        public FoodSystemProfile()
        {
            CreateMap<FoodSystem, FoodSystemDTO>().ReverseMap();
        }
    }
}
