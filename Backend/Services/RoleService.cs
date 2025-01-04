using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services;

public class RoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task CreateRoleAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
    public async Task AssignRoleToUserAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null && await _roleManager.RoleExistsAsync(roleName))
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
    public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null && await _userManager.IsInRoleAsync(user, roleName);
    }
}