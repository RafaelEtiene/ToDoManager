using AutoMapper;
using ToDoManager.Application.ViewModel;
using ToDoManager.Domain.Model;

namespace Library.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginViewModel, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => GenerateHashPassword(src.Password)));
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }

        private static string GenerateHashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}