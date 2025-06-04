namespace Shared.Events
{
    public class UserRegistrationFailedEvent
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime FailedAt { get; set; }
        public string Reason { get; set; }
    }



}
