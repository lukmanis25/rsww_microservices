using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using System.Linq.Expressions;

namespace Tours.Application;

public static class Extensions
{
    public static IConveyBuilder AddApplication(this IConveyBuilder builder)
    {
        return builder
            .AddCommandHandlers()
            .AddEventHandlers()
            .AddInMemoryCommandDispatcher()
            .AddInMemoryEventDispatcher();
    }
}