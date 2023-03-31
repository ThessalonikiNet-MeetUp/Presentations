using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    /// <summary>
    /// Custom user class that is referenced throughout the platform
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
