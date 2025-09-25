using Microsoft.AspNetCore.Identity;

namespace WebAppBach.Data

{
    public class ApplicationUser: IdentityUser
    {
        public string? FirstName { get; set; }

    }
}
