using Shared.Abstract;

namespace AccountApi.Dto_s
{
    public class UserForRegisterDto : IDTO
    {
        public string UserTaxId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
