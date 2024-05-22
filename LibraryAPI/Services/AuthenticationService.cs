using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using LibraryAPI.Constants;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<IdentityUser> _signInManager;      

        public AuthenticationService( UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _signInManager = signInManager;            
        }

        public async Task<Result<User, IEnumerable<string>>> RegisterUser(User user)
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

            var resultUser = await _userManager.CreateAsync(identityUser, user.Password);

            if (resultUser.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(identityUser,LibraryRoles.User );
                return Result.Success<User, IEnumerable<string>>(user);
            }
            return Result.Failure<User, IEnumerable<string>>(resultUser.Errors.Select(e => e.Description));

        }

        public async Task<Result<User, IEnumerable<string>>> RegisterLibrarian(User user)
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
           
            var resultUser = await _userManager.CreateAsync(identityUser, user.Password);

            if (resultUser.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(identityUser, LibraryRoles.Librarian);
                return Result.Success<User, IEnumerable<string>>(user);
            }
            return Result.Failure<User, IEnumerable<string>>(resultUser.Errors.Select(e => e.Description));

        }

        public async Task<Result<IEnumerable<string>>> Login(LoginUser loginUser)
        {
            var exist = await _userManager.FindByEmailAsync(loginUser.Email);
            if(exist == null)
            {
                return Result.Failure<IEnumerable<string>>("There is no user with :"+loginUser.Email+" email adress.");
            }
           
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);
            
            if(result.Succeeded)
            {
                string tokenString = await GenerateTokenString(loginUser);
                return Result.Success<IEnumerable<string>>(new List<string> { tokenString } );
            }
            return Result.Failure<IEnumerable<string>>("Login attempt was unsuccessful. Please try again!");
        }

        public async Task<string> GenerateTokenString(LoginUser loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            var roles = await _userManager.GetRolesAsync(user);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginUser.Email),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
