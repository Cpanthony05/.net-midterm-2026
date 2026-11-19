using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.Create;

public sealed record CreateRoomCommand(string? Name, int Capacity) : ICommand<Room>;
