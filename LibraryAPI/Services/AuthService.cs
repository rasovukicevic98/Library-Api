using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<Result<User,IEnumerable<string>>> RegisterUser(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Name,
                Email = user.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false, 
                LockoutEnabled = false,
                AccessFailedCount = 0,
            };
            
            if (!await _roleManager.RoleExistsAsync(user.Role))
            {
                List<string> potentialErrors = new List<string>();
                string potentialError = "Role "+user.Role+ " does not exist!";
                potentialErrors.Add(potentialError);
                return Result.Failure<User, IEnumerable<string>>(potentialErrors);
            }
            
            var resultUser = await _userManager.CreateAsync(identityUser, user.Password);
            
            if (resultUser.Succeeded)
            {
                var  resultRole = await _userManager.AddToRoleAsync(identityUser, user.Role);
                return Result.Success<User, IEnumerable<string>>(user);
            }
            return Result.Failure<User, IEnumerable<string>>(resultUser.Errors.Select(e => e.Description));

        }

        public async Task<Result<bool,string>> Login(LoginUser loginUser)
        {
            
            var identityUser = _userManager.FindByEmailAsync(loginUser.Email);
            if(identityUser is null)
            {
                List<string> potentialErrors = new List<string>();
                string potentialError = "User " + loginUser.Email + " does not exist!";
                potentialErrors.Add(potentialError);
                return Result.Failure<bool,string>(potentialError);
            }
            return Result.Success<bool,string>(true);
        }

        public async Task<string> GenerateTokenString(LoginUser loginUser)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginUser.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
