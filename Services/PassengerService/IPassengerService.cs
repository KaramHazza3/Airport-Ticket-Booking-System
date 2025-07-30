using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Models;

namespace FTSAirportTicketBookingSystem.Services.PassengerService;

public interface IPassengerService
{
    Task<Result<Booking>> BookFlight(Booking booking);
    Task<Result> CancelBooking(Guid bookingId);
    Task<Result<Booking>> ModifyBooking(Booking booking);
    Task<Result<List<Booking>>> GetPersonalBookings(Guid passengerId);
    Task<Result<List<Flight>>> GetFilteredFlights(Func<Flight, bool> flightFilter);
}