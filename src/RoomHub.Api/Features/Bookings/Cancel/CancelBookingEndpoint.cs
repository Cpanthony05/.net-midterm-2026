using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.Cancel;

public static class CancelBookingEndpoint
{
    public static void Map(RouteGroupBuilder group)
        => group.MapPost("/{id:guid}/cancel", Handle).WithName("CancelBooking");

    private static async Task<IResult> Handle(
        Guid id,
        ICommandHandler<CancelBookingCommand, Unit> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new CancelBookingCommand(id), cancellationToken);
        return result.IsSuccess ? Results.NoContent() : result.Error.ToProblem();
    }
}
