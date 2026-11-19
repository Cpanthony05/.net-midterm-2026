using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Features.Bookings.Get;

public static class GetBookingEndpoint
{
    public static void Map(RouteGroupBuilder group)
        => group.MapGet("/{id:guid}", Handle).WithName("GetBooking");

    private static async Task<IResult> Handle(
        Guid id,
        IQueryHandler<GetBookingQuery, Booking> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetBookingQuery(id), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToProblem();

        var booking = result.Value;
        return Results.Ok(new
        {
            booking.Id,
            booking.RoomId,
            booking.BookedBy,
            booking.StartUtc,
            booking.EndUtc,
            booking.Attendees,
            Status = booking.Status.ToString()
        });
    }
}
