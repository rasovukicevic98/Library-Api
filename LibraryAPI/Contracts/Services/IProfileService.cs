using CSharpFunctionalExtensions;
using LibraryAPI.Dto;
using System.Threading.Tasks;

namespace LibraryAPI.Contracts.Services
{
    public interface IProfileService
    {
        Task<Result<UpdateUserDto, IEnumerable<string>>> UpdateUserPasswordAsync(string email, UpdateUserDto updateUser);
        Task<Result<IEnumerable<string>>> UpdateUserProfile(UpdateProfileDto updateProfile, string email);
    }
}
