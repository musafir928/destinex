using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{

    public async Task<IEnumerable<User>> GetUsersAsync(string currentUserId)
    {
        return await context.Users.Where(u => u.Id != currentUserId).AsNoTracking().ToListAsync();
    }

    public async Task<User?> UpdateUserRoleAsync(UpdateUserDto user)
    {
        var existing = await context.Users.FindAsync(user.Id);
        if (existing == null) return null;

        existing.Role = user.Role;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return false;

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return true;
    }
}
