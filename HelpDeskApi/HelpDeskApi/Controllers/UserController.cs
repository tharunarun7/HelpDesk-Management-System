using Microsoft.AspNetCore.Authorization;
using HelpDeskApi.Models;
using HelpDeskApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userService.AddUserAsync(user);

            return Ok("User Created Successfully");
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            await _userService.UpdateUserAsync(user);

            return Ok("User Updated Successfully");
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);

            return Ok("User Deleted Successfully");
        }
    }
}
