namespace Sample.Components.Consumers;

using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;


public class NotifyRegistrationConsumer :
    IConsumer<RegistrationSubmitted>
{
  readonly ILogger<NotifyRegistrationConsumer> _logger;

  public NotifyRegistrationConsumer(ILogger<NotifyRegistrationConsumer> logger)
  {
    _logger = logger;
  }

  public Task Consume(ConsumeContext<RegistrationSubmitted> context)
  {
    if (context.Headers.TryGetHeader("testHeader", out var headerValue))
    {
      _logger.LogInformation($"Header with key testHeader and value {headerValue}");
    }
    else
    {
      _logger.LogError("Header with key testHeader should be in headers collection");
    }

    _logger.LogInformation("Member {MemberId} registered for event {EventId} on {RegistrationDate}", context.Message.MemberId, context.Message.EventId,
        context.Message.RegistrationDate);

    return Task.CompletedTask;
  }
}