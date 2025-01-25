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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApiResponse response;

        public ExerciseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetAll()
        {
            var exercises = await unitOfWork.exerciseRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);
            return Ok(map);
        }

        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> SearchByName(string Name)
        {
            var exercises = await unitOfWork.exerciseRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);
            return Ok(map);
        }

        [HttpGet("{targetMuscle:alpha}")]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> SearchByTargetMuscle(string targetMuscle)
        {
            var exercises = await unitOfWork.exerciseRepository.SearchByTargetMuscle(targetMuscle);
            var map = _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);
            return Ok(map);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ExerciseDTO>> GetByIdAsync(int Id)
        {
            var result = await unitOfWork.exerciseRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<ExerciseDTO>(result);
                return Ok(map);
            }
            response.statusCode = HttpStatusCode.NotFound;
            response.errors.Add("Exercise with this Id Not Found.");
            response.message = "a bad Request , You have made";
            return NotFound(response);
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ExerciseDTO exerciseDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Exercise>(exerciseDTO);

                await unitOfWork.exerciseRepository.AddAsync(map);
                return Ok(map);
            }
            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ExerciseDTO exerciseDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Exercise>(exerciseDTO);
                unitOfWork.exerciseRepository.Update(map);
                return Ok(map);
            }
            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var exercise = await unitOfWork.exerciseRepository.GetByIdAsync(Id);
            if (exercise is not null)
            {
                unitOfWork.exerciseRepository.Delete(exercise);
                return Ok(Id);
            }
            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);
        }
    }
}
