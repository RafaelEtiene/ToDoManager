using AutoMapper;
using ToDoManager.Application.ViewModel;
using ToDoManager.Domain.Model;

namespace Library.Application.Mapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskItem, TaskViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted));
            CreateMap<InsertTaskViewModel, TaskItem>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false));
        }
    }
}