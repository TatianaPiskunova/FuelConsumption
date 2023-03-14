using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? PositionOffice { get; set; }
    }
}
