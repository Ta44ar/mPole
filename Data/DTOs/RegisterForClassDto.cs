using mPole.Data.Models;

namespace mPole.Data.DTOs
{
    public class RegisterForClassDto
    {
        public string UserId { get; set; }
        public int ClassId { get; set; }
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;
    }
}