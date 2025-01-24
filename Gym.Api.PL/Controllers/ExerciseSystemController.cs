using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.PL.Controllers
{
    public class ExerciseSystemController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public ExerciseSystemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseSystemDTO>>> SearchByName()
        {
            var Exercises = await unitOfWork.exerciseSystemRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<ExerciseSystemDTO>>(Exercises);
            return Ok(map);
        }

        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<ExerciseSystemDTO>>> GetAll(string Name)
        {
            var Exercises = await unitOfWork.exerciseSystemRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<ExerciseSystemDTO>>(Exercises);
            return Ok(map);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ExerciseSystemDTO>> GetByIdAsync(int Id)
        {
            var result = await unitOfWork.exerciseSystemRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<ExerciseSystemDTO>(result);
                return Ok(map);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody]ExerciseSystemDTO exerciseSystemDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<ExerciseSystem>(exerciseSystemDTO);

                await unitOfWork.exerciseSystemRepository.AddAsync(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ExerciseSystemDTO exerciseSystemDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<ExerciseSystem>(exerciseSystemDTO);
                unitOfWork.exerciseSystemRepository.Update(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var Exercise = await unitOfWork.exerciseSystemRepository.GetByIdAsync(Id);
            if (Exercise is not null)
            {
                unitOfWork.exerciseSystemRepository.Delete(Exercise);
                return Ok(Id);
            }
            return BadRequest(ModelState);
        }
    }
}
