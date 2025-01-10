using Backend.DTO;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly RoleService _roleService;


    public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, RoleService roleService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _roleService = roleService;
    }

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [Authorize(Roles = "Administrator")]
    [HttpGet("admin")]
    public IActionResult GetAdminData()
    {
        return Ok("Admin data.");
    }

    [HttpPost("add")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddUser([FromBody] AddUserModel model)
    {
        if (!User.IsInRole("Administrator"))
            return Forbid("Пользователь не имеет прав администратора.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Проверяем, существует ли пользователь с таким именем
        if (await _userManager.FindByNameAsync(model.Username) != null)
            return BadRequest("Пользователь с таким именем уже существует.");

        // Создаём нового пользователя
        var user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Добавляем роли, если указаны
        if (model.Roles != null && model.Roles.Any())
        {
            foreach (var role in model.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    return BadRequest($"Роль '{role}' не существует.");

                await _userManager.AddToRoleAsync(user, role);
            }
        }

        return Ok($"Пользователь {model.Username} успешно добавлен.");
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
    public async Task<IActionResult> IsInRole(string userId, [FromQuery] string roleName)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName))
            return BadRequest("userId и roleName обязательны.");

        var isInRole = await _roleService.IsUserInRoleAsync(userId, roleName);
        return Ok(isInRole ? $"Пользователь в роли {roleName}" : $"Пользователь не в роли {roleName}");
    }
}