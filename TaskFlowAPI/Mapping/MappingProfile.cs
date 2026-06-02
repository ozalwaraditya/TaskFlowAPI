using AutoMapper;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<RegisterRequestBody, User>().ReverseMap();
    }
}