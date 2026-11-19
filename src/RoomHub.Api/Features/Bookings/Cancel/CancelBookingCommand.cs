using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.Cancel;

public sealed record CancelBookingCommand(Guid Id) : ICommand<Unit>;
