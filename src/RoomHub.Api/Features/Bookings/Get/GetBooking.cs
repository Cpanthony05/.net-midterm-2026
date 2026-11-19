using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.Get;

public sealed class GetBookingHandler(IBookingRepository bookings)
    : IQueryHandler<GetBookingQuery, Booking>
{
    public async Task<Result<Booking>> HandleAsync(
        GetBookingQuery query,
        CancellationToken cancellationToken = default)
    {
        var booking = await bookings.GetByIdAsync(query.Id, cancellationToken);
        return booking is null
            ? Result<Booking>.Failure(Error.NotFound("BookingNotFound", "The booking does not exist."))
            : Result<Booking>.Success(booking);
    }
}

