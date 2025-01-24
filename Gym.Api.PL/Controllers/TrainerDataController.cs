using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDataDTO>>> GetAll()
        {
            var trainerDatas = await unitOfWork.trainerDataRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<TrainerDataDTO>>(trainerDatas);
            return Ok(map);
        }

        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<TrainerDataDTO>>> Search(string Name)
        {
            var trainerDatas = await unitOfWork.trainerDataRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<TrainerDataDTO>>(trainerDatas);
            return Ok(map);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<TrainerDataDTO>> GetByIdAsync(int Id)
        {
            var result = await unitOfWork.trainerDataRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<TrainerDataDTO>(result);
                return Ok(map);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TrainerDataDTO trainerDataDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<TrainerData>(trainerDataDTO);

                await unitOfWork.trainerDataRepository.AddAsync(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] TrainerDataDTO trainerDataDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<TrainerData>(trainerDataDTO);
                unitOfWork.trainerDataRepository.Update(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var TrainerData = await unitOfWork.trainerDataRepository.GetByIdAsync(Id);
            if (TrainerData is not null)
            {
                unitOfWork.trainerDataRepository.Delete(TrainerData);
                return Ok(Id);
            }
            return BadRequest(ModelState);
        }
    }
}
