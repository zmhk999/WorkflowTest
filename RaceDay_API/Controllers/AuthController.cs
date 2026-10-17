
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay_API.Models;

namespace RaceDay_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayContext _context;

        private readonly PasswordHasher<UserProfile> _hasher = new();

        public AuthController(RaceDayContext context)
        {
            _context = context;
        }
        // REGISTER
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.UserPassword) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest(
                    "Username, password and email are required.");
            }

            if (dto.RoleName != "Organiser" &&
                dto.RoleName != "Participant")
            {
                return BadRequest(
                    "Role must be Organiser or Participant.");
            }

            bool exists = await _context.UserProfiles.AnyAsync(u =>
                u.UserName == dto.UserName ||
                u.Email == dto.Email);

            if (exists)
            {
                return Conflict(
                    "Username or email already exists.");
            }

            var role = await _context.UserRoles
                .FirstOrDefaultAsync(r =>
                    r.UserRoleName == dto.RoleName);

            if (role == null)
            {
                return BadRequest(
                    "The selected role does not exist.");
            }

            var profile = new UserProfile
            {
                UserProfileId = Guid.NewGuid().ToString(),
                UserRoleId = role.UserRoleId,
                UserName = dto.UserName,
                UserAge = dto.UserAge,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                RegistrationDate =
                    DateOnly.FromDateTime(DateTime.UtcNow)
            };

            profile.UserPassword = _hasher.HashPassword(
                profile,
                dto.UserPassword);

            _context.UserProfiles.Add(profile);

            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                message = "Registration successful."
            });
        }

        // LOGIN
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.UserPassword))
            {
                return BadRequest(
                    "Username and password are required.");
            }

            var profile = await _context.UserProfiles
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u =>
                    u.UserName == dto.UserName);

            if (profile == null)
            {
                return Unauthorized(
                    "Invalid username or password.");
            }

            var result = _hasher.VerifyHashedPassword(
                profile,
                profile.UserPassword,
                dto.UserPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(
                    "Invalid username or password.");
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    profile.UserProfileId),

                new Claim(
                    ClaimTypes.Name,
                    profile.UserName),

                new Claim(
                    ClaimTypes.Role,
                    profile.UserRole.UserRoleName)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc =
                        DateTimeOffset.UtcNow.AddHours(1)
                });

            return Ok(new
            {
                message = "Login successful.",
                userName = profile.UserName,
                role = profile.UserRole.UserRoleName
            });
        }

        // VIEW LOGGED-IN USER PROFILE
        [Authorize]
        [HttpGet("my-profile")]
        public IActionResult MyProfile()
        {
            return Ok(new
            {
                UserId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier),

                UserName = User.Identity?.Name,

                Role = User.FindFirstValue(
                    ClaimTypes.Role)
            });
        }

        // LOGOUT
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new
            {
                message = "Logout successful."
            });
        }
    }
}