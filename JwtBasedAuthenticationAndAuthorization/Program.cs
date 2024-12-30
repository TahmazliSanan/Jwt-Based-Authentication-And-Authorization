using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using JwtBasedAuthenticationAndAuthorization.Data;
using JwtBasedAuthenticationAndAuthorization.DataValidations.Book;
using JwtBasedAuthenticationAndAuthorization.DataValidations.Login;
using JwtBasedAuthenticationAndAuthorization.DataValidations.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JwtBasedAuthenticationAndAuthorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var issuer = builder.Configuration.GetSection("Jwt").GetSection("Issuer").Value;
            var audience = builder.Configuration.GetSection("Jwt").GetSection("Audience").Value;
            var signingKey = builder.Configuration.GetSection("Jwt").GetSection("SigningKey").Value;
            var signingKeyAsBytes = Encoding.UTF8.GetBytes(signingKey);

            // Add services to the container.
            builder.Services.AddControllers();
            
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<BookCreateRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<BookUpdateRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseNpgsql(connectionString));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKeyAsBytes)
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));

                options.AddPolicy("AdminOrUser",
                    policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(ClaimTypes.Role, "Admin") ||
                        context.User.HasClaim(ClaimTypes.Role, "User")));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
