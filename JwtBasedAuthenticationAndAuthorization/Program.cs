using FluentValidation;
using FluentValidation.AspNetCore;
using JwtBasedAuthenticationAndAuthorization.Data;
using JwtBasedAuthenticationAndAuthorization.DataValidations.Book;
using JwtBasedAuthenticationAndAuthorization.DataValidations.Login;
using JwtBasedAuthenticationAndAuthorization.DataValidations.User;
using Microsoft.EntityFrameworkCore;

namespace JwtBasedAuthenticationAndAuthorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddControllers();
            
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<BookCreateRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<BookUpdateRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseNpgsql(connectionString));

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
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
