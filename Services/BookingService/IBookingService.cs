using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Services;
using FTSAirportTicketBookingSystem.Models;

namespace FTSAirportTicketBookingSystem.Services.BookingService;

public interface IBookingService<TId> : IBaseService<Booking, TId>, IFilterService<Booking>
{
  Task<Result<ICollection<Booking>>> GetAllBookingsAsync();
  Task<Result<Booking>> AddBookingAsync(Booking booking);
  Task<Result> CancelBookingAsync(TId bookingId);
  Task<Result<Booking>> ModifyBookingAsync(TId id, Booking booking);
}