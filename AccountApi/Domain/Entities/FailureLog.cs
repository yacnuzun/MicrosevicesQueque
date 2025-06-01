using Shared.Persistance.Entities;

namespace AccountApi.Domain.Entities
{
    public class FailureLog : IEntity
    {
        public int Id { get; set; }
        public string FailedConstrait { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Reason { get; set; }
        public DateTime FailedAt { get; set; }
    }
}
