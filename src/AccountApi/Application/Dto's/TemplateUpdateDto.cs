using AccountApi.Domain.Enums;
using Shared.Abstract;

namespace AccountApi.Dto_s
{
    public class TemplateUpdateDto : IDTO
    {
        public int TemplateId { get; set; }
        public EmailTemplateType TemplateCode { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
