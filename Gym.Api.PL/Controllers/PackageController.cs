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
    public class PackageController : BaseController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public PackageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageDTO>>> GetAll()
        {
            var Package = await _UnitOfWork.packageRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<PackageDTO>>(Package);
            return Ok(map);
        }


        [AllowAnonymous]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<PackageDTO>> GetByIdAsync(int Id)
        {
            var result = await _UnitOfWork.packageRepository.GetByIdAsync(Id);
            if (result is not null)
            {
                var map = _mapper.Map<PackageDTO>(result);
                return Ok(map);
            }
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Package with this Id is not found"));
        }

        [AllowAnonymous]
        [HttpGet("{Name:alpha}")]
        public async Task<ActionResult<IEnumerable<PackageDTO>>> GetAll(string Name)
        {
            var packages = await _UnitOfWork.packageRepository.SearchByName(Name);
            var map = _mapper.Map<IEnumerable<PackageDTO>>(packages);
            return Ok(map);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]PackageDTO packageDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Package>(packageDTO);

                var count = await _UnitOfWork.packageRepository.AddAsync(map);
                if(count > 0) 
                {
                    return Ok(packageDTO);
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
        public async Task<ActionResult> Update([FromBody] PackageDTO packageDTO)
        {
            if (ModelState.IsValid)
            {
                var map = _mapper.Map<Package>(packageDTO);
                var count = await _UnitOfWork.packageRepository.Update(map);
                if (count > 0) 
                {
                    return Ok(packageDTO);
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
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var Package = await _UnitOfWork.packageRepository.GetByIdAsync(Id);
            if (Package is not null)
            {
                var count = await _UnitOfWork.packageRepository.Delete(Package);
                if (count > 0) 
                {
                    return Ok();
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Delete please try again"));
            }

            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Package with this Id is not found"));
        }
    }
}
