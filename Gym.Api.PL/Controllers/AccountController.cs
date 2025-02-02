using AutoMapper;
using Azure;
using Gym.Api.BLL.Interfaces;
using Gym.Api.BLL.Services;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Gym.Api.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Gym.Api.PL.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _TokenService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _TokenService = tokenService;
        }


        [Authorize(Roles ="Admin")]
        [HttpGet("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(e => new
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
                e.Package.Price
            }).ToList();

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

                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
            }

            return BadRequest(new ApiValidationResponse(400
                     , "a bad Request , You have made"
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
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Email is not found"));     


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
                               , "a bad Request , You have made"
                               , result.Errors.Select(e => e.Description).ToList()));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Package with this Id is not found"));

            }

            return BadRequest(new ApiValidationResponse(400
                       , "a bad Request , You have made"
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
                        var role = await _userManager.GetRolesAsync(user);
                        return Ok(new UserDTO()
                        {
                            Id = user.Id,
                            role = role[0],
                            UserName = user.UserName,
                            Token = await _TokenService.CreateTokenAsync(user, _userManager)
                        });
                    }
                    return NotFound(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Password with this Email inCorrect"));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(400
                       , "a bad Request , You have made"
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
                             , "a bad Request , You have made"
                             , result.Errors.Select(e => e.Description).ToList()));


                }

                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));


            }
            return BadRequest(new ApiValidationResponse(400
                      , "a bad Request , You have made"
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
                , "a bad Request , You have made"
                , result.Errors.Select(e => e.Description).ToList()));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
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