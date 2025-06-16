using Shared.Entities;
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
                    Name = user.Name 
                });
            }

            return Unauthorized(new { Message = "Invalid email or password." });
        } 
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] PatientInfoModel model)
        { 
            var user = await _userManager.FindByIdAsync(model.userId);
            if (user == null)
                return NotFound(new { Message = "User not authenticated." });

            // Confirm password
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
                return BadRequest(new { Message = "Password is incorrect." });

            // Check for email conflict
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null && existingUser.Id != user.Id)
                return BadRequest(new { Message = "Email is already taken by another user." });

            user.Name = model.Name;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Disease = model.Disease;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new { Message = "Profile updated successfully." });

            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        }


        // POST: api/AuthApi/signout
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "User signed out successfully." });
        } 
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PatientSecurityModel model)
        { 
            var user = await _userManager.FindByIdAsync(model.userId);
            if (user == null) return NotFound(new { Message = "User not authenticated." });

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
                return Ok(new { Message = "Password updated successfully." });

            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        }
         
        [HttpGet("user-info/{id}")]
        public async Task<IActionResult> GetUserInfo(string id)
        { 
            // Fetch the user from UserManager
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not authenticated." });

            var userModel = new PatientInfoModel
            {
                Name = user.Name,
                Email = user.Email,
                Disease = user.Disease
            }; 
            // Return only the necessary data
            return Ok(userModel);
        }

    }
}
