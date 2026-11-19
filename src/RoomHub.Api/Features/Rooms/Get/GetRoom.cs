using RoomHub.Api.Common;
using RoomHub.Api.Common.CQRS;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.Get;

public sealed class GetRoomHandler(IRoomRepository rooms) : IQueryHandler<GetRoomQuery, Room>
{
    public async Task<Result<Room>> HandleAsync(
        GetRoomQuery query,
        CancellationToken cancellationToken = default)
    {
        var room = await rooms.GetByIdAsync(query.Id, cancellationToken);
        return room is null
            ? Result<Room>.Failure(Error.NotFound("RoomNotFound", "The room does not exist."))
            : Result<Room>.Success(room);
    }
}

