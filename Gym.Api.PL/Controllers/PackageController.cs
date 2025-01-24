using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.PL.Controllers
{
    public class PackageController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public PackageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageDTO>>> GetAll()
        {
            var Package = await unitOfWork.packageRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<PackageDTO>>(Package);
            return Ok(map);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<PackageDTO>> GetByIdAsync(int Id)
        {
            var result = await unitOfWork.packageRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<PackageDTO>(result);
                return Ok(map);
            }
            return NotFound();
        }


        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<PackageDTO>>> GetAll(string Name)
        {
            var packages = await unitOfWork.packageRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<PackageDTO>>(packages);
            return Ok(map);
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PackageDTO packageDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Package>(packageDTO);

                await unitOfWork.packageRepository.AddAsync(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PackageDTO packageDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Package>(packageDTO);
                unitOfWork.packageRepository.Update(map);
                return Ok(map);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var Package = await unitOfWork.packageRepository.GetByIdAsync(Id);
            if (Package is not null)
            {
                unitOfWork.packageRepository.Delete(Package);
                return Ok(Id);
            }
            return BadRequest(ModelState);
        }
    }
}
