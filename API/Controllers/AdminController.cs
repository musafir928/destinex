using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(IUserRepository userRepository, ILogger<AdminController> logger) : BaseApiController
{
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            logger.LogError("No user id claim found, invalid token");
            return Unauthorized();
        }
        var users = await userRepository.GetUsersAsync(userIdClaim);
        logger.LogDebug($"{users.Count()} users found");
        return Ok(users.Select(u =>
        {
            return new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                DisplayName = u.DisplayName,
                Role = u.Role,
                Token = ""
            };
        }));
    }

    [HttpPost("update/role")]
    public async Task<ActionResult<UserDto>> UpdateUser(UpdateUserDto dto)
    {
        var user = await userRepository.UpdateUserRoleAsync(dto);
        if (user == null)
        {
            logger.LogError($"no user found with given info, id: {dto.Id}");
            return NotFound("no user found with given info");
        }
        return Ok(
            new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Role = user.Role,
                Token = ""
            }
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var deleted = await userRepository.DeleteUserAsync(id);
        if (!deleted)
        {
            logger.LogError($"No user found with given id: {id}");
            return NotFound();
        }

        return NoContent();
    }
}
