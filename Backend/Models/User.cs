using Microsoft.AspNetCore.Identity;

namespace Backend.Models;

public class User : IdentityUser
{
    public string FullName { get; set; }
}