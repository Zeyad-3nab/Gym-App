using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.DAL.Resources;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Gym.Api.PL.Controllers
{
    [Authorize]
    public class UserFoodController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _Mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UserFoodController(IUnitOfWork unitOfWork 
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
        [HttpPost("AddFoodToUser")]
        public async Task<ActionResult> AddFoodToUser(UserFoodDTO userFoodDTO)
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
                        return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["BadRequestMessage"]));
                    }
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["FoodIdNotFound"]));
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
        [HttpDelete("RemoveFoodFromUser")]
        public async Task<ActionResult> RemoveFoodFromUser(RemoveFoodFromUserDTO removeFood)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByIdAsync(removeFood.UserId);
                if (user is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));

                var count = await _UnitOfWork.userFoodRepository.RemoveFoodFromUser(removeFood.UserId, removeFood.FoodId);
                if (count > 0)
                    return Ok();
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["UserDon'tHaveThisFood"]));
            }
            return BadRequest(new ApiValidationResponse(400
         , _localizer["BadRequestMessage"]
         , ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage)
         .ToList()));
        }

        [Authorize]
        [HttpGet("GetFoodsForUser")]
        public async Task<ActionResult<IEnumerable<UserFoodDTO>>> GetAllFoodsForUser(string userId)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));
            var foods = await _UnitOfWork.userFoodRepository.GetAllFoodsOfUser(userId);
            var map = _Mapper.Map<IEnumerable<UserFoodDTO>>(foods);
            return Ok(map);

        }
    }
}