﻿using AutoMapper;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gym.Api.PL.Controllers
{
    public class TrainerDataController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApiResponse response;

        public TrainerDataController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
            response = new ApiResponse();
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
            response.statusCode = HttpStatusCode.NotFound;
            response.errors.Add("TrainerData with this Id Not Found.");
            response.message = "a bad Request , You have made";
            return NotFound(response);
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

            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);
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
            var TrainerData = await unitOfWork.trainerDataRepository.GetByIdAsync(Id);
            if (TrainerData is not null)
            {
                unitOfWork.trainerDataRepository.Delete(TrainerData);
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
