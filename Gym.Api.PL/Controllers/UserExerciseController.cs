using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.DAL.Resources;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Gym.Api.PL.Controllers
{
    [Authorize]
    public class UserExerciseController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _Mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UserExerciseController(IUnitOfWork unitOfWork 
                                      , UserManager<ApplicationUser> userManager 
                                      , IMapper mapper 
                                      , IStringLocalizer<SharedResources> localizer)
        {
            _UnitOfWork = unitOfWork;
            _UserManager = userManager;
            _Mapper = mapper;
            _localizer = localizer;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("AddExerciseToUser")]
        public async Task<ActionResult> AddExerciseToUser(UserExerciseDTO userExerciseDTO) 
        {
            if (ModelState.IsValid) 
            {
                if( await _UserManager.FindByIdAsync(userExerciseDTO.applicationUserId) is not null) 
                {
                    if(await _UnitOfWork.exerciseRepository.GetByIdAsync(userExerciseDTO.exerciseId) is not null) 
                    {
                        var map= _Mapper.Map<ApplicationUserExercise>(userExerciseDTO);
                        var count = await _UnitOfWork.userExerciseRepository.AddExerciseToUser(map);
                        if (count > 0) 
                        {
                            return Ok(userExerciseDTO);
                        }
                        return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest , _localizer["ErrorInSaving"]));
                    }
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["ExerciseIdNotFound"]));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));
            }
            return BadRequest(new ApiValidationResponse(400
                     , _localizer["BadRequestMessage"]
                     , ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList()));

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("RemoveExerciseFromUser")]
        public async Task<ActionResult> RemoveExerciseFromUser(RemoveExerciseFromUserDTO removeExercise) 
        {
            if (ModelState.IsValid) 
            {
                var user = await _UserManager.FindByIdAsync(removeExercise.UserId);
                if(user is null) 
                  return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));

                var count =await _UnitOfWork.userExerciseRepository.RemoveExerciseFromUser(removeExercise.UserId, removeExercise.ExerciseId);
                if (count > 0)
                    return Ok();
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["UserDon'tHaveThisExercise"]));
            }
            return BadRequest(new ApiValidationResponse(400
         , _localizer["BadRequestMessage"]
         , ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage)
         .ToList()));
        }


        [Authorize]
        [HttpGet("GetExerciseForUser")]

        public async Task<ActionResult<IEnumerable<UserExerciseDTO>>> GetAllExerciseForUser(string userId) 
        {
            var user =await _UserManager.FindByIdAsync(userId);
            if(user is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));

            var exercises = await _UnitOfWork.userExerciseRepository.GetAllExercisesOfUser(userId);
            var map = _Mapper.Map<IEnumerable<UserExerciseDTO>>(exercises);
            return Ok(map);

        }
    }
}