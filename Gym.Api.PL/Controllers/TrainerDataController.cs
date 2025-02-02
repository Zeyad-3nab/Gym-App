using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gym.Api.PL.Controllers
{
    public class TrainerDataController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public TrainerDataController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDataDTO>>> GetAll()
        {
            var trainerDatas = await unitOfWork.trainerDataRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<TrainerDataDTO>>(trainerDatas);
            return Ok(map);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<TrainerDataDTO>>> Search(string Name)
        {
            var trainerDatas = await unitOfWork.trainerDataRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<TrainerDataDTO>>(trainerDatas);
            return Ok(map);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<TrainerDataDTO>> GetByIdAsync(int Id)
        {
            var result = await unitOfWork.trainerDataRepository.GetByIdAsync(Id);
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
                var map = _mapper.Map<TrainerData>(trainerDataDTO);

                var count = await unitOfWork.trainerDataRepository.AddAsync(map);
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
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] TrainerDataDTO trainerDataDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<TrainerData>(trainerDataDTO);
                var count = await unitOfWork.trainerDataRepository.Update(map);
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
            var TrainerData = await unitOfWork.trainerDataRepository.GetByIdAsync(Id);
            if (TrainerData is not null)
            {
                var count = await unitOfWork.trainerDataRepository.Delete(TrainerData);
                if(count > 0)
                {
                    return Ok();
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Delete please try again"));
            }
            return BadRequest(new ApiValidationResponse(400
             , "a bad Request , You have made"
             , ModelState.Values
             .SelectMany(v => v.Errors)
             .Select(e => e.ErrorMessage)
             .ToList()));
        }
    }
}
