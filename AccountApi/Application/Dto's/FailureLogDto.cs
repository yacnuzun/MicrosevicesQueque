using Shared.Abstract;

namespace AccountApi.Dto_s
{
    public class FailureLogDto : IDTO
    {
        public string FailedConstrait { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Reason { get; set; }
        public DateTime FailedAt { get; set; }
    }                                    
}
