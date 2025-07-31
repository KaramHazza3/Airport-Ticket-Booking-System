namespace FTSAirportTicketBookingSystem.Common.Errors;

public class BookingErrors
{
    public static readonly Error AlreadyExists = new("Booking.AlreadyExists", "The booking is already exists");
    public static readonly Error NotFound = new("Booking.NotFound", "The booking doesn't exist");
}