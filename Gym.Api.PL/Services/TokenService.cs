using Gym.Api.BLL.Services;
using Gym.Api.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gym.Api.PL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _Configuration;

        public TokenService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var authClaim = new List<Claim>()
            {
				// Claim => User لل property هيا عباره عن شويه

				new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.GivenName , user.UserName),
                new Claim(ClaimTypes.Email , user.Email)
            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));

            }
            // Key
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:SecretKey"]));

            // Token
            var Token = new JwtSecurityToken(
                issuer: _Configuration["JWT:Issuer"],
                audience: _Configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_Configuration["JWT:DurationInDays"])),
                claims: authClaim,
                 signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
