using AccountApi.Domain.Enums;
using Shared.Abstract;

namespace AccountApi.Dto_s
{
    public class TemplateAddDto : IDTO
    {
        public EmailTemplateType TemplateCode { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
