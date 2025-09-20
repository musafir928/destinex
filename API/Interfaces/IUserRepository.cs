using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync(string currentUserId);
    Task<User?> UpdateUserRoleAsync(UpdateUserDto user);
    Task<bool> DeleteUserAsync(string id);
}
