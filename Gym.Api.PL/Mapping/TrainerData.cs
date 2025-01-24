using AutoMapper;
using Gym.Api.PL.DTOs;

namespace Gym.Api.PL.Mapping
{
    public class TrainerData:Profile
    {

        public TrainerData()
        {
            CreateMap<TrainerData , TrainerDataDTO>().ReverseMap();
        }
    }
}
