using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Features.Bookings.Create;

public static class CreateBookingEndpoint
{
    public static void Map(RouteGroupBuilder group)
        => group.MapPost("/{roomId:guid}/bookings", Handle).WithName("CreateBooking");

    private static async Task<IResult> Handle(
        Guid roomId,
        CreateBookingRequest request,
        ICommandHandler<CreateBookingCommand, Booking> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(
            new CreateBookingCommand(roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees),
            cancellationToken);
        if (result.IsFailure)
            return result.Error.ToProblem();

        var booking = result.Value;
        return Results.Created(
            $"/api/bookings/{booking.Id}",
            new { booking.Id, booking.RoomId, Status = booking.Status.ToString() });
    }
}
