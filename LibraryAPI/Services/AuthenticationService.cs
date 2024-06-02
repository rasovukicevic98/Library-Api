using AutoMapper;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using LibraryAPI.Constants;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
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
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(UserManager<User> userManager, IConfiguration config, SignInManager<User> signInManager, IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
            _mapper = mapper;
            _authenticationRepository = authenticationRepository;
        }

        public async Task<Result<RegisterUserDto, IEnumerable<string>>> RegisterUser(RegisterUserDto user)
        {
            var identityUser = _mapper.Map<User>(user);

            var resultUser = await _authenticationRepository.RegisterNewUserAsync(identityUser);

            if (resultUser.Succeeded)
            {
                var resultRole = await _authenticationRepository.AssignLibrarianRoleAsync(identityUser);
                return Result.Success<RegisterUserDto, IEnumerable<string>>(user);
            }
            return Result.Failure<RegisterUserDto, IEnumerable<string>>(resultUser.Errors.Select(e => e.Description));
        }

        public async Task<Result<RegisterUserDto, IEnumerable<string>>> RegisterLibrarian(RegisterUserDto user)
        {
            var identityUser = _mapper.Map<User>(user);

            var resultUser = await _authenticationRepository.RegisterNewUserAsync(identityUser);

            if (resultUser.Succeeded)
            {
                var resultRole = await _authenticationRepository.AssignUserRoleAsync(identityUser);
                return Result.Success<RegisterUserDto, IEnumerable<string>>(user);
            }
            return Result.Failure<RegisterUserDto, IEnumerable<string>>(resultUser.Errors.Select(e => e.Description));

        }

        public async Task<Result<IEnumerable<string>>> Login(LoginUser loginUser)
        {
            var exist = await _userManager.FindByEmailAsync(loginUser.Email);
            if (exist == null)
            {
                return Result.Failure<IEnumerable<string>>("There is no user with :" + loginUser.Email + " email adress.");
            }

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);

            if (result.Succeeded)
            {
                string tokenString = await GenerateTokenString(loginUser);
                return Result.Success<IEnumerable<string>>(new List<string> { tokenString });
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
