using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [Authorize (Roles = "Admin")]
        [HttpGet("getusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = new ResponseDTO();

            try
            {
                var users = await userService.GetAllUsers();

                response.IsSuccess = true;
                response.Message = "Users fetched successfully";
                response.Response = users;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }
        
        
    }
}