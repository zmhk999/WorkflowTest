using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay_API.Models;

namespace RaceDay_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrganiserController : ControllerBase
    {

        private readonly RaceDayContext _context;

        private readonly PasswordHasher<UserProfile> _hasher = new();

        public OrganiserController(RaceDayContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Organiser")] // ang logged-in user
        [HttpGet("organiser-only")]
        public IActionResult OrganiserOnly()
        {
            return Ok("Hello Organiser! You are authorizd.");
        }


        //[HttpPost("registerProfile")]
        //public IActionResult RegisterProfile(UserProfile dto) //uses the data object for register (email
        //{
        //    //actual User object is created using the user input form RegisterDto
        //    var newP = new UserProfile
        //    {
        //        UserProfileId = dto.UserProfileId,
        //        UserName = dto.UserName,
        //        UserAge = dto.UserAge,
        //        Email = dto.Email,
        //        PhoneNumber = dto.PhoneNumber,
        //        RegistrationDate = dto.RegistrationDate,
        //    };
        //    newP.UserPassword = _hasher.HashPassword(newP, dto.UserPassword);
        //    // newUser.PasswordHash = _hasher.HashPassword(newUser, dto.Password);//using inbuilt hash

        //    _context.UserProfiles.Add(newP); //store the hashed password to the db
        //    _context.SaveChanges();
        //    return Ok("ProfileRegistered");
        //}



        [HttpPost("registerProfile")]
        public IActionResult RegisterProfile([FromBody] UserProfile dto)
        {
            if (dto == null)
            {
                return BadRequest("Profile data is required.");
            }

            var roleExists = _context.UserRoles.Any(r => r.UserRoleId == dto.UserRoleId);

            if (!roleExists)
            {
                return BadRequest("Invalid UserRoleId.");
            }

            var newP = new UserProfile
            {
                UserProfileId = Guid.NewGuid().ToString(),
                UserRoleId = dto.UserRoleId,
                UserName = dto.UserName,
                UserAge = dto.UserAge,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            newP.UserPassword = _hasher.HashPassword(
                newP, dto.UserPassword
            );

            _context.UserProfiles.Add(newP);
            _context.SaveChanges();

            return Ok("ProfileRegistered");
        }





        //private readonly RaceDayContext _context;
        //public OrganiserController(RaceDayContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public IActionResult GetUserRoles()
        //{
        //    var tasks = _context.UserRoles.ToList();
        //    return Ok(tasks);
        //    //201
        //}


        //[HttpGet("get/{UserRoleId}")]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult GetUserRole(string UserRoleId)
        //{
        //    var userRole = _context.UserRoles.FirstOrDefault(t => t.UserRoleId == UserRoleId);
        //    if (userRole == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    return Ok(userRole);
        //}



    }
}


//[HttpPost("register")]
//public IActionResult Register(UserRole dto) //uses the data object for register (email
//{
//    //actual User object is created using the user input form RegisterDto
//    var newUser = new UserRole { UserRoleName = dto.UserRoleName, UserFirstName = dto.UserFirstName, UserLastName  = dto.UserLastName };
//   // newUser.PasswordHash = _hasher.HashPassword(newUser, dto.Password);//using inbuilt hash

//    _context.UserRoles.Add(newUser); //store the hashed password to the db
//    _context.SaveChanges();
//    return Ok("Registered");
//}