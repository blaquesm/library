using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class AddUserModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FullName { get; set; }

    public List<string> Roles { get; set; } = new();

}