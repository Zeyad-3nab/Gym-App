using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.BLL.Repositories;
using Gym.Api.BLL.Services;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gym.Api.PL.Controllers
{
    public class TrainerDataController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly ITokenService _TokenService;

        public TrainerDataController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
            _UserManager = userManager;
            _TokenService = tokenService;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDataDTO>>> GetAll()
        {
            var trainerDatas = await _UnitOfWork.trainerDataRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<TrainerDataDTO>>(trainerDatas);
            return Ok(map);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<TrainerDataDTO>>> Search(string Name)
        {
            var trainerDatas = await _UnitOfWork.trainerDataRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<TrainerDataDTO>>(trainerDatas);
            return Ok(map);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<TrainerDataDTO>> GetByIdAsync(int Id)
        {
            var result = await _UnitOfWork.trainerDataRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<TrainerDataDTO>(result);
                return Ok(map);
            }
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Trainer Data with this Id is not found"));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TrainerDataDTO trainerDataDTO)
        {
            if (ModelState.IsValid)
            {
                var package = await _UnitOfWork.packageRepository.GetByIdAsync(trainerDataDTO.PackageId);
                if (package is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "PackageId With this id is not found"));

                var user = await _UserManager.FindByEmailAsync(trainerDataDTO.Email);
                if (user is not null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Email is not found"));


                var map = _mapper.Map<TrainerData>(trainerDataDTO);
                var appUser = new ApplicationUser() 
                {
                    UserName = trainerDataDTO.UserName,
                    Email = trainerDataDTO.Email,
                    Address = trainerDataDTO.Address,
                    Long = trainerDataDTO.Long,
                    Weight = trainerDataDTO.Weight,
                    Age = trainerDataDTO.Age,
                    StartPackage = trainerDataDTO.StartPackage , 
                    EndPackage = trainerDataDTO.EndPackage ,
                    PackageId = trainerDataDTO.PackageId,
                };

                var result = await _UserManager.CreateAsync(appUser , trainerDataDTO.Password);    //Create Account
                if (result.Succeeded)
                {
                      await _UserManager.AddToRoleAsync(appUser, "Trainer");
                      
                      var trainerSend = await _UnitOfWork.trainerDataRepository.AddAsync(map);
                      if (trainerSend > 0) 
                      {
                          var ReturnedUser = new UserDTO()
                          {
                      
                              Id = appUser.Id,
                              role = "Trainer",
                              UserName = appUser.UserName,
                              Token = await _TokenService.CreateTokenAsync(appUser, _UserManager)
                          };
                          return Ok(ReturnedUser);
                      }
                      
                      
                      return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                           , "Error in save TrainerData"));
                }

                return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                           , "a bad Request , You have made"
                           , result.Errors.Select(e => e.Description).ToList()));
               
            }
            return BadRequest(new ApiValidationResponse(400
            , "a bad Request , You have made"
            , ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList()));

        }
    

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] TrainerDataDTO trainerDataDTO)
        {
            if (ModelState.IsValid)
            {
                var package = _UnitOfWork.packageRepository.GetByIdAsync(trainerDataDTO.PackageId);
                if (package is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Package With this Id is not found"));

                var map = _mapper.Map<TrainerData>(trainerDataDTO);
                var count = await _UnitOfWork.trainerDataRepository.Update(map);
                if (count > 0)
                {
                    return Ok(trainerDataDTO);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save please try again"));
            }
            return BadRequest(new ApiValidationResponse(400
         , "a bad Request , You have made"
         , ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage)
         .ToList()));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var TrainerData = await _UnitOfWork.trainerDataRepository.GetByIdAsync(Id);
            if (TrainerData is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "TrainerData with this Id is not found"));

            var count = await _UnitOfWork.trainerDataRepository.Delete(TrainerData);
            if (count > 0)
            {
                return Ok();
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Delete please try again"));
            
        }
    }
}
