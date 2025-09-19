using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtension
{
    public static UserDto ToDto(this User user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Role = user.Role,
            Email = user.Email,
            Token = tokenService.CreateToken(user),
        };
    }
}
