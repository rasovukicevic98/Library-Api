using AutoMapper;
using CSharpFunctionalExtensions;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class UserService : IUserService
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {            
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<LoginUserDto> GetUserAsync(string id)
        {
            User user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            LoginUserDto userDto = _mapper.Map<LoginUserDto>(user);
            return userDto;
        }

        public async Task<List<LoginUserDto>> GetUsersAsync()
        {
            List<User> users = await _userRepository.GetUsersAsync();
            if (users == null)
            {
                return null;
            }
            var result = _mapper.Map<List<LoginUserDto>>(users);
            return result;
        }
    }
}
