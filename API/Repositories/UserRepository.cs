using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<ActionResult<User?>> GetUserByIdAsync(string id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<ActionResult<User?>> GetUserForUpdate(string id)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ActionResult<IReadOnlyList<User>>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<ActionResult<bool>> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(User user)
    {
        context.Entry(user).State = EntityState.Modified;
    }
}
