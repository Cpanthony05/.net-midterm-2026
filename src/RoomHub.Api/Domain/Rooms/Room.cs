using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Rooms;

public sealed class Room
{
    private Room() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Capacity { get; private set; }

    public static Result<Room> Create(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Room>.Failure(Error.Validation("InvalidName", "Room name is required."));
        if (capacity <= 0)
            return Result<Room>.Failure(Error.Validation("InvalidCapacity", "Capacity must be positive."));

        return Result<Room>.Success(new Room
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Capacity = capacity
        });
    }

    public Result<Unit> CheckAvailability(DateTime startUtc, DateTime endUtc, IEnumerable<Booking> activeBookings)
    {
        if (activeBookings.Any(booking =>
                startUtc < booking.EndUtc && booking.StartUtc < endUtc))
        {
            return Result<Unit>.Failure(
                Error.Conflict("RoomOccupied", "The room is occupied in this interval."));
        }

        return Result<Unit>.Success(Unit.Value);
    }
}

