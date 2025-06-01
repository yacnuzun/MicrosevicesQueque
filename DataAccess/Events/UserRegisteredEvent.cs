using Castle.Core.Smtp;
using Shared.Helpers.Mailing;

namespace Shared.Events
{
    public class UserRegisteredEvent
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }



}
