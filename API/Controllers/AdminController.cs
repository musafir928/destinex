using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(IUserRepository userRepository) : BaseApiController
{
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
        var users = await userRepository.GetUsersAsync(userIdClaim);
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
        if (user == null) return NotFound("no user found with given info");
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
        if (!deleted) return NotFound();

        return NoContent();
    }
}
