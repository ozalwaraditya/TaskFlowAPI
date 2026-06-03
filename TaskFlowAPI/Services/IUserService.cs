using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Services;


public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<UserDto?> LoginUser(LoginRequestBody loginRequestBody);
    Task<UserDto?> RegisterUser(RegisterRequestBody registerRequestBody);
}