using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay_API.Models;
using System.Security.Claims;

namespace RaceDay_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthenticatorController : ControllerBase
    {

        private readonly RaceDayContext _context;
        public AuthenticatorController(RaceDayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUserRoles()
        {
            var tasks = _context.UserRoles.ToList();
            return Ok(tasks);
            //201
        }


        [HttpGet("get/{UserRoleId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUserRole(string UserRoleId)
        {
            var userRole = _context.UserRoles.FirstOrDefault(t => t.UserRoleId == UserRoleId);
            if (userRole == null)
            {
                return NotFound();
            }
            else
                return Ok(userRole);
        }


        [HttpPost("create")]
        public IActionResult CreateTask([FromBody] UserRole newUserRole) // says that content will be semt from the r
        {
            newUserRole.UserRoleId = Guid.NewGuid().ToString();
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUserRole(string id)
        {
            var userRole = _context.UserRoles
                .FirstOrDefault(t => t.UserRoleId == id);

            if (userRole == null)
            {
                return NotFound();
            }

            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

            return NoContent();
        }

        //[HttpPost("registerRole")]
        //public IActionResult Register(UserRole dto) //uses the data object for register (email
        //{
        //    //actual User object is created using the user input form RegisterDto
        //    var newUser = new UserRole {UserRoleId = dto.UserRoleId, UserRoleName = dto.UserRoleName, 
        //        UserFirstName = dto.UserFirstName, UserLastName = dto.UserLastName };
        //   // newUser.PasswordHash = _hasher.HashPassword(newUser, dto.Password);//using inbuilt hash

        //    _context.UserRoles.Add(newUser); //store the hashed password to the db
        //    _context.SaveChanges();
        //    return Ok("RoleRegistered");
        //}

        /// <summary>
        /// ////////////////////
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("registerRole")]
        public IActionResult RegisterRole([FromBody] UserRole dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var newUserRole = new UserRole
            {
                UserRoleId = Guid.NewGuid().ToString(),
                UserRoleName = dto.UserRoleName,
                UserFirstName = dto.UserFirstName,
                UserLastName = dto.UserLastName
            };

            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetUserRole),
                new { UserRoleId = newUserRole.UserRoleId },
                newUserRole
            );
        }



        //[HttpPost("login")]
        //public async Task<IActionResult> Login(UserRole dto)
        //{
        //    //looks up
        //    var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
        //    if (user == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        //    if (result == PasswordVerificationResult.Failed)
        //    {
        //        return Unauthorized();
        //    }


    }
}
