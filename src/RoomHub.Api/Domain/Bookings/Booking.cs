namespace RoomHub.Api.Domain.Bookings;

using RoomHub.Api.Domain.Common;

public sealed class Booking
{
    private Booking() { }

    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    public static Result<Booking> Create(
        Guid roomId,
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        int roomCapacity,
        DateTime nowUtc)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return Result<Booking>.Failure(Error.Validation("InvalidBookedBy", "BookedBy is required."));
        if (endUtc <= startUtc)
            return Result<Booking>.Failure(Error.Validation("InvalidRange", "The end must be after the start."));
        if (attendees <= 0)
            return Result<Booking>.Failure(Error.Validation("InvalidAttendees", "Attendees must be positive."));
        if (attendees > roomCapacity)
            return Result<Booking>.Failure(
                Error.Validation("AttendeesExceedCapacity", "The room does not have enough seats."));
        if (startUtc < nowUtc)
            return Result<Booking>.Failure(
                Error.Validation("StartInPast", "A booking cannot start in the past."));
        var maximumDuration = roomCapacity > 10
            ? TimeSpan.FromHours(2)
            : TimeSpan.FromHours(4);
        if (endUtc - startUtc > maximumDuration)
            return Result<Booking>.Failure(
                Error.Validation(
                    "DurationTooLong",
                    $"A booking cannot last longer than {maximumDuration.TotalHours:0} hours for this room."));

        return Result<Booking>.Success(new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            BookedBy = bookedBy.Trim(),
            StartUtc = startUtc,
            EndUtc = endUtc,
            Attendees = attendees,
            Status = BookingStatus.Active
        });
    }

    public Result<Unit> Cancel(DateTime nowUtc)
    {
        if (Status == BookingStatus.Cancelled)
            return Result<Unit>.Failure(
                Error.Conflict("BookingAlreadyCancelled", "The booking is already cancelled."));
        if (StartUtc - nowUtc < TimeSpan.FromMinutes(60))
            return Result<Unit>.Failure(
                Error.Conflict(
                    "CancellationTooLate",
                    "A booking can only be cancelled at least 60 minutes before it starts."));

        Status = BookingStatus.Cancelled;
        return Result<Unit>.Success(Unit.Value);
    }
}
