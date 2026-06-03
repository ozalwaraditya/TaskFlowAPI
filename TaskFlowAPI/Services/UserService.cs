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

    public async Task<UserDto?> LoginUser(LoginRequestBody loginRequestBody)
    {
        var dbUser = await _userRepository.GetUserByEmail(loginRequestBody.Email);
        
        if(dbUser == null) 
            return new UserDto();
        
        var isValid = BCrypt.Net.BCrypt.Verify(loginRequestBody.Password, dbUser.Password);
       
        return !isValid ? new UserDto() : _mapper.Map<UserDto>(dbUser);
    }

    public async Task<UserDto?> RegisterUser(RegisterRequestBody registerRequestBody)
    {
        var existingUser = await _userRepository.GetUserByEmail(registerRequestBody.Email);

        if (existingUser is not null)
            return _mapper.Map<UserDto>(existingUser);

        var user = _mapper.Map<User>(registerRequestBody);
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerRequestBody.Password);

        var createdUser = await _userRepository.AddUser(user);

        return _mapper.Map<UserDto>(createdUser);
    }
}