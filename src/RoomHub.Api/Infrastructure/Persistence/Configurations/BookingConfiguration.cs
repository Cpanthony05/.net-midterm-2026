using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(booking => booking.Id);
        builder.Property(booking => booking.BookedBy).IsRequired().HasMaxLength(200);
        builder.Property(booking => booking.Status).IsRequired().HasConversion<string>();
        builder.HasIndex(booking => new { booking.RoomId, booking.Status, booking.StartUtc, booking.EndUtc });
    }
}

