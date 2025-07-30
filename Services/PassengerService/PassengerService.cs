using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Errors;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Repository;

namespace FTSAirportTicketBookingSystem.Services.PassengerService;

public class PassengerService : IPassengerService
{
    private readonly IRepository _repository;

    public PassengerService(IRepository repository)
    {
        this._repository = repository;
    }
    public async Task<Result<Booking>> BookFlight(Booking booking)
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        var bookingList = bookings.ToList();
        var isExist = bookingList.Any(b => b.PassengerId == booking.PassengerId && b.FlightId == booking.FlightId);
        return isExist ? BookingErrors.AlreadyExists : await this._repository.WriteAsync(booking);
    }

    public async Task<Result> CancelBooking(Guid bookingId)
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

    public async Task<Result<Booking>> ModifyBooking(Booking booking)
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        var bookingList = bookings.ToList();
        var isExist = bookingList.Exists(b => b.Id == booking.Id);
        return isExist ?  await this._repository.UpdateAsync(booking): BookingErrors.NotFound;
    }

    public async Task<Result<List<Booking>>> GetPersonalBookings(Guid passengerId)
    {
        var bookings = await this._repository.ReadAsync<Booking>();
        var bookingList = bookings.ToList();
        return bookingList.Where(p => p.PassengerId == passengerId).ToList();
    }

    public async Task<Result<List<Flight>>> GetFilteredFlights(Func<Flight, bool> flightFilter)
    {
        var flights = await this._repository.ReadAsync<Flight>();
        var flightsList = flights.ToList();
        return flightsList.Where(flightFilter).ToList();
    }
}