using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DocDirectApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthApiController : ControllerBase
    {
        private readonly UserManager<Patient> _userManager;
        private readonly SignInManager<Patient> _signInManager;

        public AuthApiController(UserManager<Patient> userManager, SignInManager<Patient> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // POST: api/AuthApi/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest(new { Message = "Email is already registered." });

            var user = new Patient
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return Ok(new { Message = "Patient registered successfully." });

            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);

            return BadRequest(ModelState);
        }

        // POST: api/AuthApi/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // Get the user info
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return Unauthorized(new { Message = "User not found." });

                return Ok(new
                {
                    Message = "Login successful.",
                    UserId = user.Id,
                    Email = user.Email,
                    Name = user.Name // Assuming your Patient class has a Name property
                });
            }

            return Unauthorized(new { Message = "Invalid email or password." });
        }

        // POST: api/AuthApi/signout
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "User signed out successfully." });
        }
        [Authorize] // Requires the user to be authenticated
        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            // Get the authenticated user's ID from claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User not authenticated." });

            // Fetch the user from UserManager
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            // Return only the necessary data
            return Ok(new
            {
                UserId = user.Id,
                Name = user.Name // Adjust based on your user model property
            });
        }

    }
}
