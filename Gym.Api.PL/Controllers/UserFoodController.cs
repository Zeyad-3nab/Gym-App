using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.PL.Controllers
{
    public class UserFoodController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _Mapper;

        public UserFoodController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _UserManager = userManager;
            _Mapper = mapper;
        }

        [HttpPost("AddFoodToUser")]
        public async Task<ActionResult> AddExerciseToUser(UserFoodDTO userFoodDTO)
        {
            if (ModelState.IsValid)
            {
                if (await _UserManager.FindByIdAsync(userFoodDTO.applicationUserId) is not null)
                {
                    if (await _UnitOfWork.foodRepository.GetByIdAsync(userFoodDTO.foodId) is not null)
                    {
                        var map = _Mapper.Map<ApplicationUserFood>(userFoodDTO);
                        var count = await _UnitOfWork.userFoodRepository.AddFoodToUser(map);
                        if (count > 0)
                        {
                            return Ok(userFoodDTO);
                        }
                        return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "a bad Request , You have made"));
                    }
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Food with this Id is not found"));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
            }
            return BadRequest(new ApiValidationResponse(400
                     , "a bad Request , You have made"
                     , ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList()));

        }


        [HttpDelete("RemoveFoodFromUser")]
        public async Task<ActionResult> RemoveExerciseFromUser(RemoveFoodFromUserDTO removeFood)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByIdAsync(removeFood.UserId);
                if (user is null)
                {
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this id is not found"));
                }
                var count = await _UnitOfWork.userFoodRepository.RemoveFoodFromUser(removeFood.UserId, removeFood.FoodId);
                if (count > 0)
                    return Ok();
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "User don't have this exercise"));
            }
            return BadRequest(new ApiValidationResponse(400
         , "a bad Request , You have made"
         , ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage)
         .ToList()));
        }

        [HttpGet("GetFoodsForUser")]

        public async Task<ActionResult<IEnumerable<UserFoodDTO>>> GetAllFoodsForUser(string userId)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
            var foods = await _UnitOfWork.userFoodRepository.GetAllFoodsOfUser(userId);
            var map = _Mapper.Map<IEnumerable<UserFoodDTO>>(foods);
            return Ok(map);

        }
    }
}
