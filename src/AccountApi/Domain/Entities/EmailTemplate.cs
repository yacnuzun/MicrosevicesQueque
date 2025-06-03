using Shared.Persistance.Entities;

namespace AccountApi.Domain.Entities
{
    public class EmailTemplate :  IEntity
    {
        public int Id { get; set; }
        public string TemplateCode { get; set; } // örn: "ACCOUNT_CREATED"
        public string Subject { get; set; }
        public string Body { get; set; } // HTML olabilir
        public bool IsActive { get; set; } = true;
    }
}
