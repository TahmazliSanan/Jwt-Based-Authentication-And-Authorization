using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtBasedAuthenticationAndAuthorization.Data;
using JwtBasedAuthenticationAndAuthorization.Entities;
using JwtBasedAuthenticationAndAuthorization.Payloads.Login;
using JwtBasedAuthenticationAndAuthorization.Payloads.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JwtBasedAuthenticationAndAuthorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AccountController(IConfiguration config, AppDbContext context)
        {
            _config = config;
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
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            LoginResponse loginResponse;
            var foundUser = await _context
                .Users
                .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(loginRequest.Email.ToLower()) && u.Password.Equals(loginRequest.Password));

            if (foundUser is null)
            {
                loginResponse = new LoginResponse
                {
                    Message = "Invalid email or password"
                };
            }
            else
            {
                loginResponse = new LoginResponse
                {
                    Message = "Login successful",
                    UserId = foundUser.Id,
                    Jwt = GenerateJwt(foundUser)
                };
            }

            return Ok(loginResponse);
        }

        private string GenerateJwt(User user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var signingKey = _config.GetSection("Jwt").GetSection("SigningKey").Value;
            var signingKeyAsBytes = Encoding.UTF8.GetBytes(signingKey);

            var claimList = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),
                new(ClaimTypes.Email, user.Email)
            };

            if (user.BirthDateTime.HasValue)
            {
                claimList.Add(new Claim("BirthDateTime", user.BirthDateTime.Value.ToString("O")));
            }

            claimList.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimList),
                Issuer = "example.com",
                Audience = "example.com",
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signingKeyAsBytes),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var createdToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var jwt = jwtSecurityTokenHandler.WriteToken(createdToken);
            return jwt;
        }
    }
}
