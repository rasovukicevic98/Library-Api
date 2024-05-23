using AutoMapper;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginUserDto, IdentityUser>().ReverseMap();
            CreateMap<UpdateUser, IdentityUser>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BooksDto>().ReverseMap();
            CreateMap<BookRent, BookRentDto>().ReverseMap();
        }
    }
}
