using AutoMapper;
using Azure;
using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Models;
using Gym.Api.PL.DTOs;
using Gym.Api.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly ApiResponse response;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            response = new ApiResponse();
        }


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

        [AllowAnonymous]
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

                response.statusCode = HttpStatusCode.NotFound;
                response.errors.Add("User with this Id Not Found.");
                response.message = "a bad Request , You have made";
                return NotFound(response);
            }

            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (user is not null)
                {
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.errors.Add("User with this email already exists.");
                    response.message = "a bad Request , You have made";
                    return BadRequest(response);
                }
                var CheckPackage=await _unitOfWork.packageRepository.GetByIdAsync(registerDTO.PackageId);
                if(CheckPackage is not null) 
                {
                    var appUser = _mapper.Map<ApplicationUser>(registerDTO);

                    var result = await _userManager.CreateAsync(appUser, registerDTO.Password);    //Create Account
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(appUser, "Trainer"); 
                        return Ok();
                    }

                    else
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.errors = new List<string>();

                        foreach (var err in result.Errors)
                        {
                            response.errors.Add(err.Description);
                        }
                        response.message = "a bad Request , You have made";
                        return BadRequest(response);
                    }
                }
                else 
                {
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.errors.Add("Package with this Id NotFound");
                    response.message = "a bad Request , You have made";
                    return BadRequest(response);
                }
               
            }

            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);
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
                        var claims = new List<Claim>();

                        //claims.Add(new Claim("tokenNo", "75"));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item.ToString()));
                        }


                        //SigningCradiential

                        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var SigningCradiential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);


                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddMonths(1),
                            signingCredentials: SigningCradiential
                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Ok(_token);
                    }
                    else
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.errors.Add("Password with this email InCorrect");
                        response.message = "a bad Request , You have made";
                        return BadRequest(response);
                    }
                }
                else
                {
                    response.statusCode = HttpStatusCode.NotFound;
                    response.errors.Add("Email with this Name NotFound.");
                    response.message = "a bad Request , You have made";
                    return BadRequest(response);
                }
            }
            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);
        }


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
                    else
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.errors = new List<string>();

                        foreach (var err in result.Errors)
                        {
                            response.errors.Add(err.Description);
                        }
                        response.message = "a bad Request , You have made";
                        return BadRequest(response);
                    }


                }

                response.statusCode = HttpStatusCode.BadRequest;
                response.errors.Add("User with this Id NotFound.");
                response.message = "a bad Request , You have made";
                return BadRequest(response);


            }
            response.statusCode = HttpStatusCode.BadRequest;
            response.errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(response);

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
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.errors = new List<string>();

                    foreach (var err in result.Errors)
                    {
                        response.errors.Add(err.Description);
                    }
                    response.message = "a bad Request , You have made";
                    return BadRequest(response);
                }
                response.statusCode = HttpStatusCode.NotFound;
                response.errors.Add("User with this Id Not Found");
                response.message = "a bad Request , You have made";
                return NotFound(response);
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