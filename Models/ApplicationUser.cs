using Microsoft.AspNetCore.Identity;

namespace GameModel{
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }
}
}