using System.Security.Cryptography;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var usersData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<SeedUserDto>>(usersData);

        if (users == null)
        {
            Console.WriteLine("No users in seed data");
            return;
        }

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            var newUser = new User
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Role = user.Role,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(newUser);
        }

        await context.SaveChangesAsync();
    }
}