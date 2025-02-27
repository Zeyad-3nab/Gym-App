﻿using AutoMapper;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;

namespace Gym.Api.PL.Mapping
{
    public class ApplicationProfile:Profile
    {

        public ApplicationProfile(IConfiguration configuration)
        {
            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
            CreateMap<Food, FoodDTO>().ReverseMap();
            CreateMap<Package, PackageDTO>().ReverseMap();
            CreateMap<TrainerData, TrainerDataDTO>().ReverseMap();
            CreateMap<ApplicationUser, RegisterDTO>().ReverseMap();
            CreateMap<ApplicationUser, UpdateuserDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserReturnDTO>() 
           .ForMember(e => e.ReminderOfPackage, opt => opt.MapFrom(src => (src.EndPackage.Date-DateTime.Now).Days)); // Map ReminderOfPackage

            CreateMap<ApplicationUserExercise, UserExerciseDTO>().ReverseMap();
            CreateMap<ApplicationUserFood, UserFoodDTO>().ReverseMap();
            CreateMap<ExerciseSystemItem, ExerciseSystemItemDTO > ()
            .ForMember(e => e.ExerciseName, opt => opt.MapFrom(src => src.exercise.Name)); // Map ReminderOfPackage
        }
    }
}