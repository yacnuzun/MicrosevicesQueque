using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Enums;
using AccountApi.Infrastructure.Helpers.Mail.Implemantations;
using MassTransit;
using Microsoft.Extensions.Options;
using Shared.Events;
using Shared.Helpers.Mailing;

namespace AccountApi.Infrastructure.Helpers.Consumer
{
    public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly IOptions<MailSettings> _emailSettings;
        private readonly ILogger<AccountMailSenderStrategy> _log;
        private readonly ITemplateMailService _templateService;

        public UserRegisteredConsumer(ITemplateMailService templateService, 
            IOptions<MailSettings> emailSettings, 
            ILogger<AccountMailSenderStrategy> log)
        {
            _templateService = templateService;
            _emailSettings = emailSettings;
            _log = log;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var user = context.Message;

            var template = await _templateService.GetMailTemplate(EmailTemplateType.WelcomeUser.ToString());
            if (template == null) return;

            string body = template.Data.Body.Replace("{KULLANICI_ADI}", user.FullName);

            string subject = template.Data.Subject;

            var strategy = new AccountMailSenderStrategy(_emailSettings, _log);

            var mailContext = new MailSenderContext();

            mailContext.SetStrategy(strategy);

            var isResult = await mailContext.SendMailAsync
                (
                        new EmailMessage
                        {
                            Body = body,
                            Subject = subject,
                            Contacts = context.Message.Email,
                        }
                );
        }
    }
}
