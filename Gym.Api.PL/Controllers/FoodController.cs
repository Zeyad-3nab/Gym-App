using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.BLL.Repositories;
using Gym.Api.DAL.Models;
using Gym.Api.DAL.Resources;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Gym.Api.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Gym.Api.PL.Controllers
{
    [Authorize]
    public class FoodController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public FoodController(IUnitOfWork unitOfWork, IMapper mapper , IStringLocalizer<SharedResources> localizer)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodDTO>>> GetAll()
        {
            var foods = await _UnitOfWork.foodRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<FoodDTO>>(foods);
            return Ok(map);
        }


        [Authorize]
        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<FoodDTO>>> SearchByName(string Name)
        {
            var foods = await _UnitOfWork.foodRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<FoodDTO>>(foods);
            return Ok(map);
        }


        [Authorize]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<FoodDTO>> GetByIdAsync(int Id)
        {
            var result = await _UnitOfWork.foodRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<FoodDTO>(result);
                return Ok(map);
            }
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["FoodIdNotFound"]));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Add(FoodDTO foodDTO)
        {
            if (ModelState.IsValid)
            {
                if (foodDTO.Image is not null)
                {
                    foodDTO.ImageURL = DocumentSettings.Upload(foodDTO.Image, "FoodImages");   // خزن الصوره وهات اسمها
                }

                var map = _mapper.Map<Food>(foodDTO);

                var count = await _UnitOfWork.foodRepository.AddAsync(map);
                if (count > 0)
                {
                    return Ok(foodDTO);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["ErrorInSaving"]));
            }

            return BadRequest(new ApiValidationResponse(400
                    , _localizer["BadRequestMessage"]
                    , ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()));
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> Update(FoodDTO foodDTO)
        {
            if (ModelState.IsValid)
            {
                if (foodDTO.ImageURL is not null)
                    DocumentSettings.Delete(foodDTO.ImageURL, "FoodImages");  //Upload Image to wwwroot


                if (foodDTO.Image is not null)
                    foodDTO.ImageURL = DocumentSettings.Upload(foodDTO.Image, "FoodImages");   //Save image && return name


                var map = _mapper.Map<Food>(foodDTO);
                var count = await _UnitOfWork.foodRepository.Update(map);
                if (count > 0)
                {
                    return Ok(foodDTO);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["ErrorInSaving"]));
            }
            return BadRequest(new ApiValidationResponse(400
                     , _localizer["BadRequestMessage"]
                     , ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList()));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var food = await _UnitOfWork.foodRepository.GetByIdAsync(Id);
            if (food is not null)
            {
                var count = await _UnitOfWork.foodRepository.Delete(food);
                if (count > 0)
                {
                    return Ok(Id);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["ErrorInSaving"]));
            }
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["FoodIdNotFound"]));
        }

    }
}
