using Microsoft.AspNetCore.Identity;

namespace Backend.DTO;

public class User : IdentityUser
{
    public string FullName { get; set; }
}