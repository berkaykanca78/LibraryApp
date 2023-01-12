using AutoMapper;
using LibraryApp.Common.Extensions;
using LibraryApp.Data.Dtos.Author;
using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Data.Entities;
using System.Linq;

namespace LibraryApp.Mapper.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //int id = int.Parse(ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value);
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.DisplayName()))
                .ReverseMap();
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Address.StreetName))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.DisplayName()))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.Address.FullAddress))
                .ReverseMap();
            CreateMap<User, UserForRegisterDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Address.StreetName))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.Address.FullAddress))
                .ReverseMap();
            CreateMap<Book, BookForListDto>()
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                 .ForMember(dest => dest.MediaTypeName, opt => opt.MapFrom(src => src.MediaType.DisplayName()))
                 .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.Language.DisplayName()))
                 .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name))
                 .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate))
                 .ReverseMap();
            CreateMap<Author, AuthorForListDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                 .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.DisplayName()))
                .ReverseMap();            
            CreateMap<Author, AuthorForDetailDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ReverseMap();
            CreateMap<Book, BookForDetailDto>()
                .ReverseMap();
        }
    }
}
