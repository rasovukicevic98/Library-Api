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
            CreateMap<BookRent, BookRentHistoryDto>()
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ReverseMap();
            CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => 0));
        }
    }
}
