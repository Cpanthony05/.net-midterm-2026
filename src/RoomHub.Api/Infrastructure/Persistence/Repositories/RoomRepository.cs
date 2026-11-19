using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class RoomRepository(RoomHubDbContext db) : IRoomRepository
{
    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => db.Rooms.FirstOrDefaultAsync(room => room.Id == id, cancellationToken);

    public Task AddAsync(Room room)
    {
        db.Rooms.Add(room);
        return Task.CompletedTask;
    }
}

