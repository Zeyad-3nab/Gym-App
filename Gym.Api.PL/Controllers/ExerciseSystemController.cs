using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.PL.Controllers
{
    public class ExerciseSystemController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseSystemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<IEnumerable<ExerciseSystem>>> GetAll() 
        {
            var exerciseSystems =  await _unitOfWork.exerciseSystemRepository.GetAllAsync();
            return Ok(exerciseSystems);
        }

        public async Task<ActionResult<ExerciseSystem>> GetById(int ExerciseSystemId)
        {
            var exerciseSystems = await _unitOfWork.exerciseSystemRepository.GetById(ExerciseSystemId);
            return Ok(exerciseSystems);
        }



    }
}
