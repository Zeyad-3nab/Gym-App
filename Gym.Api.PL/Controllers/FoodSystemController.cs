using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.PL.Controllers
{
    public class FoodSystemController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public FoodSystemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodSystemDTO>>> GetAll()
        {
            var Systems = await unitOfWork.foodSystemRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<FoodSystemDTO>>(Systems);
            return Ok(map);
        }

        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<FoodSystem>>> GetAll(string Name)
        {
            var foodSystems = await unitOfWork.foodSystemRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<FoodSystemDTO>>(foodSystems);
            return Ok(map);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<FoodSystemDTO>> GetByIdAsync(int Id)
        {
            var result = await unitOfWork.foodSystemRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<FoodSystemDTO>(result);
                return Ok(map);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] FoodSystemDTO foodSystamDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<FoodSystem>(foodSystamDTO);

                await unitOfWork.foodSystemRepository.AddAsync(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] FoodSystemDTO foodSystamDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<FoodSystem>(foodSystamDTO);
                unitOfWork.foodSystemRepository.Update(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var FoodSystem =await unitOfWork.foodSystemRepository.GetByIdAsync(Id);
            if(FoodSystem is not null)
            {
                unitOfWork.foodSystemRepository.Delete(FoodSystem);
                return Ok(Id);
            }
            return BadRequest(ModelState);
        }
    }
}
