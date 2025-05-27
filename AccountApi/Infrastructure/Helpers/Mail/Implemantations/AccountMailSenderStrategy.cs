using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Shared.Helpers.Mailing;

namespace AccountApi.Infrastructure.Helpers.Mail.Implemantations
{
    public class AccountMailSenderStrategy : IMailSenderStrategy
    {
        private readonly MailSettings _settings;
        private readonly ILogger<AccountMailSenderStrategy> _logger;

        public AccountMailSenderStrategy(IOptions<MailSettings> settings, ILogger<AccountMailSenderStrategy> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<bool> SendAsync(EmailMessage emailModel)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_settings.SenderMail));
                var mailList = emailModel.Contacts.Split(',');
                foreach (var item in mailList)
                {
                    email.To.Add(MailboxAddress.Parse(item));
                }
                email.Subject = emailModel.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailModel.Body };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_settings.SenderMail, _settings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                _logger.LogInformation("[MailService] Mail gönderildi -> {To}", emailModel.Contacts);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[MailService] Hata: {ErrorType} - {Message}", ex.GetType().Name, ex.Message);
                return false;
            }
        }
    }
}
