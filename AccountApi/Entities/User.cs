using Shared.Interfaces;

namespace AccountApi.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserTaxID { get; set; }
        public Byte[] PasswordSalt { get; set; }
        public Byte[] PasswordHash { get; set; }
        public bool Status { get; set; }

    }
}
