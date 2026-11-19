using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Features.Bookings.Create;

public sealed record CreateBookingCommand(
    Guid RoomId,
    string? BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees) : ICommand<Booking>;
