using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Models
{
    public class Group
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public DateTime? RegularClassTime { get; set; }
        public int CapacityLimit { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
        public Group()
        {
            CapacityLimit = 5;
        }
    }
}

