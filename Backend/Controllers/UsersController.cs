using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly RoleService _roleService;

    public UsersController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost("manage-users")]
    [Authorize(Roles = "Administrator")]
    public IActionResult ManageUsers()
    {
        return Ok("Пользователи обновлены");
    }

    [HttpPost("{userId}/assign-role")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AssignRole(string userId, string roleName)
    {
        await _roleService.AssignRoleToUserAsync(userId, roleName);
        return Ok($"Роль {roleName} назначена пользователю с ID {userId}");
    }

    [HttpGet("{userId}/is-in-role")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> IsInRole(string userId, string roleName)
    {
        var isInRole = await _roleService.IsUserInRoleAsync(userId, roleName);
        return Ok(isInRole ? $"Пользователь в роли {roleName}" : $"Пользователь не в роли {roleName}");
    }
}