using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class BookingRepository(RoomHubDbContext db) : IBookingRepository
{
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => db.Bookings.FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Booking>> GetActiveForRoomAsync(
        Guid roomId,
        CancellationToken cancellationToken = default)
        => await db.Bookings
            .Where(booking => booking.RoomId == roomId && booking.Status == BookingStatus.Active)
            .ToListAsync(cancellationToken);

    public Task<int> CountActiveForParticipantOnDateAsync(
        string bookedBy,
        DateTime dayStartUtc,
        CancellationToken cancellationToken = default)
        => db.Bookings.CountAsync(
            booking => booking.BookedBy == bookedBy
                       && booking.Status == BookingStatus.Active
                       && booking.StartUtc >= dayStartUtc
                       && booking.StartUtc < dayStartUtc.AddDays(1),
            cancellationToken);

    public Task AddAsync(Booking booking)
    {
        db.Bookings.Add(booking);
        return Task.CompletedTask;
    }
}
