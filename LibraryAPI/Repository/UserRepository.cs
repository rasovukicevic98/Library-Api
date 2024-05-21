using AutoMapper;
using CSharpFunctionalExtensions;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(DataContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            return null;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            List<IdentityUser> users = await _userManager.Users.ToListAsync();
            if (users != null)
            {
                var  result = _mapper.Map<List<UserDto>>(users);
                return result;
            }
            return null;            
        }

        public bool RegisterUser(User user)
        {
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<Result<IEnumerable<string>>> UpdateUsernameAsync(UpdateProfile updateProfile, string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user.UserName.Equals(updateProfile.UserName)) return Result.Failure<IEnumerable<string>>("You entered the same username.");
            
            if (await _userManager.FindByNameAsync(updateProfile.UserName) != null) return Result.Failure<IEnumerable<string>>("Username is alredy taken.");

            user.UserName = updateProfile.UserName;
            user.Email = updateProfile.UserName;
            user.PhoneNumber = updateProfile.PhoneNumber;
            await _userManager.UpdateAsync(user);

            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());
        }

        public async Task<Result<UpdateUser, IEnumerable<string>>> UpdateUserPasswordAsync(string id,UpdateUser updateUser)
        {
            IdentityUser user=await  _userManager.FindByIdAsync(id);            
            
            var identityUser = await _userManager.FindByIdAsync(id);
            var res = await _userManager.CheckPasswordAsync(identityUser, updateUser.OldPassword);
            if (!res)
            {
                return Result.Failure<UpdateUser, IEnumerable<string>>(new List<string> { "Old password is incorrect." });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, updateUser.NewPassword);
            if (result.Succeeded)
            {
                return Result.Success<UpdateUser, IEnumerable<string>>(updateUser);
            }

            return Result.Failure<UpdateUser, IEnumerable<string>>(result.Errors.Select(e => e.Description));
        }
    }
}
