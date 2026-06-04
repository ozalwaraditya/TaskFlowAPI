using Microsoft.AspNetCore.Mvc;
using TaskFlowAPI.Services;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllUsers());
        }

        // GET /api/users/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var user = await _userService.GetUserById(userId);
            return user == null ? NotFound() : Ok(user);
        }

        // DELETE /api/users/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await _userService.DeleteUser(userId);
            return result ? NoContent() : NotFound();
        }
    }
}