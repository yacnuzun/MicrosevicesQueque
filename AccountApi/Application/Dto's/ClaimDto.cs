using AccountApi.Domain.Enums;
using Shared.Abstract;

namespace AccountApi.Dto_s
{
    public class ClaimDto : IDTO
    {
        public UserRoles Role { get; set; }
    }
}
