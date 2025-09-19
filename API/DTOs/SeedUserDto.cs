using System;
using System.Text.Json.Serialization;
using API.Enums;
using Microsoft.AspNetCore.Identity;

namespace API.DTOs;

public class SeedUserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string DisplayName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Role Role { get; set; } = Role.User;
}
