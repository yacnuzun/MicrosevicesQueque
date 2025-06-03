using AccountApi.Application.Services.Interfaces;
using AccountApi.Dto_s;
using MassTransit;
using Shared.Events;

namespace AccountApi.Infrastructure.Helpers.Consumer
{
    public class UserRegistrationFailedConsumer : IConsumer<UserRegistrationFailedEvent>
    {
        private readonly IFailureLogService _failureLogService;
        private readonly ILogger<UserRegistrationFailedConsumer> _log;

        public UserRegistrationFailedConsumer(IFailureLogService failureLogService, ILogger<UserRegistrationFailedConsumer> log)
        {
            _failureLogService = failureLogService;
            _log = log;
        }

        public async Task Consume(ConsumeContext<UserRegistrationFailedEvent> context)
        {
            try
            {
                var result = await _failureLogService.ExistFailure(context.Message.Email);

                if (!result.Success)
                {
                    await _failureLogService.LogFailureAsync(new FailureLogDto
                    {
                        FailedConstrait = context.Message.Email + context.Message.FullName + "Consumer Exception" + DateTime.Now,
                        Email = context.Message.Email,
                        FullName = context.Message.FullName,
                        Reason = "Consumer Exception",
                        FailedAt = DateTime.UtcNow
                    });
                }

            }
            catch (Exception ex)
            {
                _log.LogError(ex, "UserRegistrationFailedConsumer içinde hata: {Message}", ex.Message);

                throw;
            }


        }
    }

}
