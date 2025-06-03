using Shared.Persistance.Entities;

namespace AccountApi.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserTaxID { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

    }
}
