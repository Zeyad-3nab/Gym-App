using AutoMapper;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;

namespace Gym.Api.PL.Mapping
{
    public class PackageProfile:Profile
    {
        public PackageProfile()
        {
            CreateMap<Package, PackageDTO>().ReverseMap();
        }
    }
}
