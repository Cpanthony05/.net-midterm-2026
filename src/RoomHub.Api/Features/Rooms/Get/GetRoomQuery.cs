using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.Get;

public sealed record GetRoomQuery(Guid Id) : IQuery<Room>;
