using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Enums;
using AccountApi.Infrastructure.Helpers.Mail.Implemantations;
using MassTransit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Events;
using Shared.Helpers.Mailing;

namespace AccountApi.Infrastructure.Helpers.Consumer
{
    public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly IOptions<MailSettings> _emailSettings;
        private readonly ILogger<AccountMailSenderStrategy> _log;
        private readonly ITemplateMailService _templateService;
        private readonly IFailureLogService _failureLogService;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserRegisteredConsumer(ITemplateMailService templateService,
            IOptions<MailSettings> emailSettings,
            ILogger<AccountMailSenderStrategy> log,
            IFailureLogService failureLogService,
            IPublishEndpoint publishEndpoint)
        {
            _templateService = templateService;
            _emailSettings = emailSettings;
            _log = log;
            _failureLogService = failureLogService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            try
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
            catch (Exception ex)
            {

                UserRegistrationFailedEvent inner = new UserRegistrationFailedEvent
                {
                    Email = context.Message.Email,
                    FullName = context.Message.FullName,
                    FailedAt = DateTime.UtcNow,
                    Reason = "Consumer Excepiton"
                };


                if (context.GetRetryCount() >= 2)
                {
                    if (inner is not null||!string.IsNullOrEmpty(inner.Email))
                    {
                      await  _publishEndpoint.Publish(inner);
                    }
                    return;

                }

                throw;
            }

        }
    }
    
}
