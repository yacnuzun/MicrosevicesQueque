using Shared.Persistance.Entities;

namespace AccountApi.Domain.Entities
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
