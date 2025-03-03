using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCQRS.Application.Event.UserCreation
{
    //#EventsLearn 1
    public record UserCreatedEvent(Guid UserId) : INotification;
    
}
