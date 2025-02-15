using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.DAL.Resources;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Gym.Api.PL.Controllers
{
    public class ExerciseSystemItemController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ExerciseSystemItemController(IUnitOfWork unitOfWork , IMapper mapper , IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ExerciseSystemItem>>> GetAll()
        {
            var exercises =  await _unitOfWork.exerciseSystemItemRepository.GetAllAsync();
            return Ok(exercises);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetById")]
        public async Task<ActionResult<ExerciseSystemItem>> GetById(int Id)
        {
            var exercises = await _unitOfWork.exerciseSystemItemRepository.GetByIdAsync(Id);
            return Ok(exercises);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Add(ExerciseSystemItemDTO exerciseSystemItemDTO) 
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
                                  , _localizer["BadRequestMessage"]
                                  , ModelState.Values
                                  .SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList()));

            var map = _mapper.Map<ExerciseSystemItem>(exerciseSystemItemDTO);

            var count = await _unitOfWork.exerciseSystemItemRepository.AddAsync(map);
            if (count > 0)
                return Ok();

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["ErrorInSaving"]));

        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<int>> Update(ExerciseSystemItemDTO exerciseSystemItemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
                                  , _localizer["BadRequestMessage"]
                                  , ModelState.Values
                                  .SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList()));

            var exerciseSystemItem = await _unitOfWork.exerciseSystemItemRepository.GetByIdAsync(exerciseSystemItemDTO.Id);

            if (exerciseSystemItem is null)
                return NotFound();

            exerciseSystemItem = _mapper.Map<ExerciseSystemItem>(exerciseSystemItemDTO);

            var count = await _unitOfWork.exerciseSystemItemRepository.Update(exerciseSystemItem);
            if (count > 0)
                return Ok();

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["ErrorInSaving"]));
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        public async Task<ActionResult<int>> Delete(int Id) 
        {
            var exerciseSystemItem = await _unitOfWork.exerciseSystemItemRepository.GetByIdAsync(Id);

            if (exerciseSystemItem is null)
                return NotFound();

            var count = await _unitOfWork.exerciseSystemItemRepository.Delete(exerciseSystemItem);
            if (count > 0)
                return Ok();

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["ErrorInSaving"]));
        }
    }
}
