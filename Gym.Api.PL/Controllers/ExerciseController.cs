using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gym.Api.PL.Controllers
{
    public class ExerciseController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public ExerciseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetAll()
        {
            var exercises = await _UnitOfWork.exerciseRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);
            return Ok(map);
        }

        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> SearchByName(string Name)
        {
            var exercises = await _UnitOfWork.exerciseRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);
            return Ok(map);
        }

        [HttpGet("{targetMuscle:alpha}")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> SearchByTargetMuscle(string targetMuscle)
        {
            var exercises = await _UnitOfWork.exerciseRepository.SearchByTargetMuscle(targetMuscle);
            var map = _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);
            return Ok(map);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ExerciseDTO>> GetByIdAsync(int Id)
        {
            var result = await _UnitOfWork.exerciseRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<ExerciseDTO>(result);
                return Ok(map);
            }
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Exercise with this Id is not found"));
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ExerciseDTO exerciseDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Exercise>(exerciseDTO);

                var count = await _UnitOfWork.exerciseRepository.AddAsync(map);
                if(count > 0)
                {
                    return Ok(exerciseDTO);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in saveing please try again"));
            }
            return BadRequest(new ApiValidationResponse(400
                       , "a bad Request , You have made"
                       , ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage)
                       .ToList()));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ExerciseDTO exerciseDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Exercise>(exerciseDTO);
                var count = await _UnitOfWork.exerciseRepository.Update(map);
                if (count > 0) 
                {
                    return Ok(exerciseDTO);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in saveing please try again"));
            }
            return BadRequest(new ApiValidationResponse(400
                     , "a bad Request , You have made"
                     , ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList()));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var exercise = await _UnitOfWork.exerciseRepository.GetByIdAsync(Id);
            if (exercise is not null)
            {
                var count = await _UnitOfWork.exerciseRepository.Delete(exercise);
                if (count > 0)
                {
                    return Ok(Id);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in saveing please try again"));
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
