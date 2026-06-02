using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtSettings _jwtSettings;
    private readonly JwtTokenProvider _jwtTokenProvider;

    public AuthController(IUserService userService, IOptions<JwtSettings> jwtOptions)
    {
        _userService = userService;
        _jwtTokenProvider = new JwtTokenProvider(jwtOptions);
    }

    [HttpPost("login")]
    public async Task<ResponseDTO> Login([FromBody] LoginRequestBody loginRequest)
    {
        var user = await _userService.LoginUser(loginRequest);

        if (user == null)
        {
            return new ResponseDTO
            {
                IsSuccess = false,
                Message = "Invalid email or password."
            };
        }

        var accessToken = _jwtTokenProvider.GenerateJwtToken(user);

        return new ResponseDTO
        {
            IsSuccess = true,
            Message = "User logged in successfully!",
            Response = accessToken
        };
    }

    [HttpPost("register")]
    public async Task<ResponseDTO> Register([FromBody] RegisterRequestBody registerRequest)
    {
        var user = await _userService.RegisterUser(registerRequest);

        if (user is null)
        {
            return new ResponseDTO
            {
                IsSuccess = false,
                Message = "Registration failed."
            };
        }

        var accessToken = _jwtTokenProvider.GenerateJwtToken(user);

        return new ResponseDTO
        {
            IsSuccess = true,
            Message = "User registered successfully!",
            Response = accessToken
        };
    }
    
    
}