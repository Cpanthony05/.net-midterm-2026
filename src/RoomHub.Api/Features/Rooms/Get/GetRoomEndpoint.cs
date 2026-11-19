using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.Get;

public static class GetRoomEndpoint
{
    public static void Map(RouteGroupBuilder group)
        => group.MapGet("/{id:guid}", Handle).WithName("GetRoom");

    private static async Task<IResult> Handle(
        Guid id,
        IQueryHandler<GetRoomQuery, Room> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetRoomQuery(id), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToProblem();

        var room = result.Value;
        return Results.Ok(new { room.Id, room.Name, room.Capacity });
    }
}
