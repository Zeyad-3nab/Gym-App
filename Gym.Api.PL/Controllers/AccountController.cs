using AutoMapper;
using Azure;
using Gym.Api.BLL.Interfaces;
using Gym.Api.BLL.Repositories;
using Gym.Api.BLL.Services;
using Gym.Api.DAL.Models;
using Gym.Api.DAL.Resources;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Gym.Api.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;

namespace Gym.Api.PL.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _TokenService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            IStringLocalizer<SharedResources> localizer)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _TokenService = tokenService;
            _localizer = localizer;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.Select(e => new
            {
                e.Id,
                e.UserName,
                e.Email,
                e.Long,
                e.Weight,
                e.Age,
                e.StartPackage,
                e.EndPackage,
                e.Package.Name,
                e.Package.Duration,
                e.Package.OldPrice,
                e.Package.NewPrice,
                e.Gender
            }).ToListAsync();

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetNewUsers")]
        public async Task<ActionResult> GetNewUsers()
        {
            var users = await _userManager.Users.Where(u => u.foods.Count() == 0 || u.exercises.Count() == 0).Select(e => new
            {
                e.Id,
                e.UserName,
                e.Email,
                e.Long,
                e.Weight,
                e.Age,
                e.StartPackage,
                e.EndPackage,
                e.Package.Name,
                e.Package.Duration,
                e.Package.OldPrice,
                e.Package.NewPrice,
                e.Gender
            }).ToListAsync();

            return Ok(users);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetUsersNearExpirePackage")]
        public async Task<ActionResult> GetUsersNearExpirePackage()
        {
            var targetDate = DateTime.UtcNow.Date.AddDays(10);
            var users = await _userManager.Users.Where(u=>u.EndPackage <= targetDate).Select(e => new
            {
                e.Id,
                e.UserName,
                e.Email,
                e.Long,
                e.Weight,
                e.Age,
                e.StartPackage,
                e.EndPackage,
                e.Package.Name,
                e.Package.Duration,
                e.Package.OldPrice,
                e.Package.NewPrice,
                e.Gender
            }).ToListAsync();

            return Ok(users);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetUserById")]
        public async Task<ActionResult> GetUserById(string userId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is not null)
                {
                    return Ok(user);
                }

                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));
            }

            return BadRequest(new ApiValidationResponse(400
                     , _localizer["BadRequestMessage"]
                     , ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList()));
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (user is not null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserEmailNotFound"]));

                var package = await _unitOfWork.packageRepository.GetByIdAsync(registerDTO.PackageId);
                if (package is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["PackageIdNotFound"]));

                var CheckPackage=await _unitOfWork.packageRepository.GetByIdAsync(registerDTO.PackageId);
                if(CheckPackage is not null) 
                {
                    var appUser = _mapper.Map<ApplicationUser>(registerDTO);

                    var result = await _userManager.CreateAsync(appUser, registerDTO.Password);    //Create Account
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(appUser, "Trainer");

                        var ReturnedUser = new UserDTO()
                        {

                            Id = appUser.Id,
                            role = "Trainer",
                            UserName = appUser.UserName,
                            Token = await _TokenService.CreateTokenAsync(appUser , _userManager)
                        };
                        return Ok(ReturnedUser);
                    }

                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                               , _localizer["BadRequestMessage"]
                               , result.Errors.Select(e => e.Description).ToList()));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["PackageIdNotFound"]));

            }

            return BadRequest(new ApiValidationResponse(400
                       , _localizer["BadRequestMessage"]
                       , ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage)
                       .ToList()));
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                    {
                        var userDTO = new UserDTO()
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            role="Trainer",
                            Token = await _TokenService.CreateTokenAsync(user, _userManager)
                        };
                        var checkTrainer = await _userManager.IsInRoleAsync(user, "Admin");
                        if (checkTrainer)
                            userDTO.role = "Admin";
                        return Ok(userDTO);
                    }
                    return NotFound(new ApiErrorResponse(StatusCodes.Status400BadRequest, _localizer["ErrorInPassword"]));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserEmailNotFound"]));
            }
            return BadRequest(new ApiValidationResponse(400
                       , _localizer["BadRequestMessage"]
                       , ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage)
                       .ToList()));
        }


        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody]UpdateuserDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(registerDTO.Id);

                if (user is not null)
                {
                    var package = _unitOfWork.packageRepository.GetByIdAsync(registerDTO.PackageId);
                    if (package is null)
                        return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["PackageIdNotFound"]));

                    user.Address= registerDTO.Address;
                    user.PhoneNumber = registerDTO.PhoneNumber;
                    user.Long=registerDTO.Long;
                    user.Age= registerDTO.Age;
                    user.Weight=registerDTO.Weight;
                    user.Address=registerDTO.Address;
                    user.StartPackage=registerDTO.StartPackage;
                    user.EndPackage=registerDTO.EndPackage;
                    user.PackageId=registerDTO.PackageId;


                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok("Updated");
                    }
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                             , _localizer["BadRequestMessage"]
                             , result.Errors.Select(e => e.Description).ToList()));


                }

                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));


            }
            return BadRequest(new ApiValidationResponse(400
                      , _localizer["BadRequestMessage"]
                      , ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)
                      .ToList()));

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                , _localizer["BadRequestMessage"]
                , result.Errors.Select(e => e.Description).ToList()));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, _localizer["UserIdNotFound"]));
            }

            return BadRequest(new ApiValidationResponse(400
                      , _localizer["BadRequestMessage"]
                      , ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)
                      .ToList()));
        }
    }
}