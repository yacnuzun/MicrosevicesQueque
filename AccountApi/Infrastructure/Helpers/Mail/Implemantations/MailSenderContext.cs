using Shared.Helpers.Mailing;

namespace AccountApi.Infrastructure.Helpers.Mail.Implemantations
{
    public class MailSenderContext
    {
        private IMailSenderStrategy _strategy;

        public void SetStrategy(IMailSenderStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<bool> SendMailAsync(EmailMessage email)
        {
            if (_strategy == null) throw new InvalidOperationException("Mail gönderim stratejisi belirlenmedi.");
            return await _strategy.SendAsync(email);
        }
    }
}
