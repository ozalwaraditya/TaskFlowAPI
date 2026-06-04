using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Services;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserById(int userId)
    {
        var user = await _userRepository.GetUserById(userId);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> LoginUser(LoginRequestBody loginRequestBody)
    {
        var dbUser = await _userRepository.GetUserByEmail(loginRequestBody.Email);

        if (dbUser == null)
            return null;

        var isValid = BCrypt.Net.BCrypt.Verify(loginRequestBody.Password, dbUser.Password);

        return !isValid ? null : _mapper.Map<UserDto>(dbUser);
    }

    public async Task<UserDto?> RegisterUser(RegisterRequestBody registerRequestBody)
    {
        var existingUser = await _userRepository.GetUserByEmail(registerRequestBody.Email);

        if (existingUser != null)
            return null;

        var user = _mapper.Map<User>(registerRequestBody);
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerRequestBody.Password);
        user.CreatedAt = DateTime.UtcNow;

        var createdUser = await _userRepository.AddUser(user);

        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<bool> DeleteUser(int userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
            return false;

        await _userRepository.DeleteUser(user);
        await _userRepository.Save();

        return true;
    }
}