using AutoMapper;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<RegisterRequestBody, User>().ReverseMap();
        CreateMap<RegisterRequestBody, RegisterRequestBody>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<TaskItem, TaskItemDto>().ReverseMap();
        CreateMap<ProjectMember, ProjectMemberDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

    }
}