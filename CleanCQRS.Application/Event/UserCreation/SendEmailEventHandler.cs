using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCQRS.Application.Event.UserCreation
{
    //#EventsLearn 2
    public class SendEmailEventHandler(ILogger<SendEmailEventHandler> logger) 
        : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("User created {UserId}: Send email start", notification.UserId);
            await Task.Delay(2000, cancellationToken);
            logger.LogInformation("User created {UserId}: Send email end", notification.UserId);
        }
    }
}
