using Microsoft.AspNetCore.Identity;

namespace mPole.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public byte[] ProfileImage { get; set; } = Array.Empty<byte>();
        public virtual ICollection<Class> InstructedClasses { get; set; } = new List<Class>();
        public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
