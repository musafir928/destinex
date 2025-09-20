using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AuthController(
    AppDbContext context,
    ITokenService tokenService,
    ILogger<AuthController> logger
) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        logger.LogInformation("Register attempt for {Email}", registerDto.Email);

        if (await EmailExists(registerDto.Email))
        {
            logger.LogWarning("Register failed: Email {Email} already exists", registerDto.Email);
            return BadRequest("Email existed.");
        }

        using var hmac = new HMACSHA512();
        var user = new User
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            Role = Enums.Role.User,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        logger.LogInformation("User {Email} registered successfully with Id {UserId}", user.Email, user.Id);

        return user.ToDto(tokenService);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        logger.LogInformation("Login attempt for {Email}", loginDto.Email);

        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);
        if (user == null)
        {
            logger.LogWarning("Login failed: Email {Email} not found", loginDto.Email);
            return Unauthorized("Invalid email address");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                logger.LogWarning("Login failed: Invalid password for {Email}", loginDto.Email);
                return Unauthorized("Invalid password.");
            }
        }

        logger.LogInformation("User {Email} logged in successfully", user.Email);

        return user.ToDto(tokenService);
    }

    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
    }
}
