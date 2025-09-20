using API.Enums;

namespace API.DTOs;

public class UserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public required Role Role { get; set; }
    public required string Token { get; set; }
}

public class UpdateUserDto
{
    public required string Id { get; set; }
    public required Role Role { get; set; }
}

