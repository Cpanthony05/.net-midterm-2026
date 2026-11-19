using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Bookings.Create;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, Booking>
{
    public async Task<Result<Booking>> HandleAsync(
        CreateBookingCommand command,
        CancellationToken cancellationToken = default)
    {
        var room = await rooms.GetByIdAsync(command.RoomId, cancellationToken);
        if (room is null)
            return Result<Booking>.Failure(
                Error.NotFound("RoomNotFound", "The room does not exist."));

        var bookingResult = Booking.Create(
            command.RoomId,
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            room.Capacity,
            timeProvider.GetUtcNow().UtcDateTime);
        if (bookingResult.IsFailure)
            return bookingResult;

        var bookingDate = bookingResult.Value.StartUtc.Date;
        var activeBookingsOnDate = await bookings.CountActiveForParticipantOnDateAsync(
            bookingResult.Value.BookedBy,
            bookingDate,
            cancellationToken);
        if (activeBookingsOnDate >= 2)
            return Result<Booking>.Failure(
                Error.Conflict(
                    "DailyBookingLimitReached",
                    "A participant cannot have more than 2 active bookings on the same day."));

        var availability = room.CheckAvailability(
            command.StartUtc,
            command.EndUtc,
            await bookings.GetActiveForRoomAsync(command.RoomId, cancellationToken));
        if (availability.IsFailure)
            return Result<Booking>.Failure(availability.Error);

        await bookings.AddAsync(bookingResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return bookingResult;
    }
}
