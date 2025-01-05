using System.ComponentModel.DataAnnotations;

namespace mPole.Data.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ClassId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

        public virtual ApplicationUser User { get; set; }
        public virtual Class Class { get; set; }
    }

    public enum RegistrationStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
}