using JwtBasedAuthenticationAndAuthorization.Data;
using JwtBasedAuthenticationAndAuthorization.Entities;
using JwtBasedAuthenticationAndAuthorization.Payloads.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtBasedAuthenticationAndAuthorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            UserResponse userResponse;
            var foundUser = await _context
                .Users
                .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(userRegisterRequest.Email.ToLower()));

            if (foundUser is not null)
            {
                userResponse = new UserResponse
                {
                    Message = "User already exists with this email",
                    Id = foundUser.Id,
                };
            }
            else
            {
                var user = new User
                {
                    FirstName = userRegisterRequest.FirstName,
                    LastName = userRegisterRequest.LastName,
                    Email = userRegisterRequest.Email,
                    Password = userRegisterRequest.Password,
                    BirthDateTime = userRegisterRequest.BirthDateTime,
                    Roles = new List<string> { "User" },
                    CreatedDateTime = DateTime.UtcNow
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                userResponse = new UserResponse
                {
                    Message = "User created successfully",
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    BirthDateTime = user.BirthDateTime,
                    Roles = user.Roles,
                    CreatedDateTime = user.CreatedDateTime
                };
            }

            return Created($"/get/{userResponse.Id}", userResponse);
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}
