using CSharpFunctionalExtensions;
using LibraryAPI.Models;
using System.Threading.Tasks;

namespace LibraryAPI.Contracts.Services
{
    public interface IProfileService
    {
        Task<Result<UpdateUser, IEnumerable<string>>> UpdateUserPasswordAsync(string email, UpdateUser updateUser);
        Task<Result<IEnumerable<string>>> UpdateUserProfile(UpdateProfile updateProfile, string email);
    }
}
