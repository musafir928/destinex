using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(User user);
    Task<ActionResult<IReadOnlyList<User>>> GetUsersAsync();
    Task<ActionResult<User?>> GetUserByIdAsync(string id);
    Task<ActionResult<User?>> GetUserForUpdate(string id);

    Task<ActionResult<bool>> SaveAllAsync();
}
