using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Errors;
using FTSAirportTicketBookingSystem.Common.Services;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Repository;

namespace FTSAirportTicketBookingSystem.Services.BookingService;

public class BookingService : IBookingService, IFilterService<Booking>
{
    private readonly IRepository _repository;

    public BookingService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<Booking>> GetAsync(Func<Booking, bool> predicate)
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        var bookingList = bookings.ToList();
        var booking = bookingList.SingleOrDefault(predicate);
        return booking == null ? BookingErrors.NotFound : booking;
    }

    public async Task<Result<ICollection<Booking>>> GetAllAsync()
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        return bookings.ToList();
    }

    public async Task<Result<Booking>> AddAsync(Booking booking)
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        var bookingList = bookings.ToList();
        var isExist = bookingList.Any(b => b.Passenger.Id == booking.Passenger.Id && b.Flight.Id == booking.Flight.Id);
        return isExist ? BookingErrors.AlreadyExists : await this._repository.WriteAsync(booking);
    }

    public async Task<Result> DeleteAsync(Guid bookingId)
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        var bookingList = bookings.ToList();
        var canceledBooking = bookingList.SingleOrDefault(b => b.Id == bookingId);
        if (canceledBooking == null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }
        await this._repository.DeleteAsync(canceledBooking);
        return Result.Success();
    }

    public async Task<Result<Booking>> UpdateAsync(Guid id, Booking booking)
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        var bookingList = bookings.ToList();
        var isExist = bookingList.Exists(b => b.Id == id);
        return isExist ?  await this._repository.UpdateAsync(booking): BookingErrors.NotFound;
    }

    public async Task<Result<List<Booking>>> FilterAsync(params Func<Booking, bool>[] match)
    {
        return await FilterHelper.FilterAsync(this, match);
    }
}