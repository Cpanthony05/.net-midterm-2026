namespace RoomHub.Api.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetActiveForRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
    Task<int> CountActiveForParticipantOnDateAsync(
        string bookedBy,
        DateTime dayStartUtc,
        CancellationToken cancellationToken = default);
    Task AddAsync(Booking booking);
}
