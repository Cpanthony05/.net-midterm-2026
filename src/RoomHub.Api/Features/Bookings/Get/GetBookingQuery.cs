using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Features.Bookings.Get;

public sealed record GetBookingQuery(Guid Id) : IQuery<Booking>;
