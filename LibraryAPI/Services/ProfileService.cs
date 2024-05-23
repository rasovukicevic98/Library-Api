using CSharpFunctionalExtensions;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly UserManager<User> _userManager;

        public ProfileService(IProfileRepository profileRepository, UserManager<User> userManager)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Result<UpdateUser, IEnumerable<string>>> UpdateUserPasswordAsync(string email, UpdateUser updateUser)
        {
            User user = await _profileRepository.FindByEmailAsync(email);
          
            var res = await _profileRepository.CheckPasswordAsync(user, updateUser.OldPassword);
            if (!res)
            {
                return Result.Failure<UpdateUser, IEnumerable<string>>(new List<string> { "Old password is incorrect." });
            }

            var token = await _profileRepository.GeneratePasswordResetTokenAsync(user);

            var result = await _profileRepository.ResetPasswordAsync(user, token, updateUser.NewPassword);
            if (result.Succeeded)
            {
                return Result.Success<UpdateUser, IEnumerable<string>>(updateUser);
            }

            return Result.Failure<UpdateUser, IEnumerable<string>>(result.Errors.Select(e => e.Description));
        }

        public async Task<Result<IEnumerable<string>>> UpdateUserProfile(UpdateProfile updateProfile, string email)
        {
            User user = await _profileRepository.FindByEmailAsync(email);
            if (user.UserName.Equals(updateProfile.UserName)) return Result.Failure<IEnumerable<string>>("You entered the same username.");

            if (await _profileRepository.FindByNameAsync(updateProfile.UserName) != null) return Result.Failure<IEnumerable<string>>("Username is alredy taken.");

            user.UserName = updateProfile.UserName;
            user.Email = updateProfile.UserName;
            user.PhoneNumber = updateProfile.PhoneNumber;
            await _profileRepository.UpdateUserAsync(user);

            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());
        }

        
    }
}
