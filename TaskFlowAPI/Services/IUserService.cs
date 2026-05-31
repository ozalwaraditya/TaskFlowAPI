using TaskFlowAPI.DTOs;

namespace TaskFlowAPI.Services;


public interface IUserService
{
    Task<ResponseDTO> GetAllUsers();
}