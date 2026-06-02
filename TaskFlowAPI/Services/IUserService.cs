using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Services;


public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsers();
    Task<UserDTO?> LoginUser(LoginRequestBody loginRequestBody);
    Task<UserDTO?> RegisterUser(RegisterRequestBody registerRequestBody);
}