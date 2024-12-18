using Microsoft.AspNetCore.Identity;

namespace mPole.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public byte[] ProfileImage { get; set; } = Array.Empty<byte>();
    }
}
