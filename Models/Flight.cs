using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Models;

public class Flight
{
    public Guid Id { get; set; }
    public decimal BasePrice { get; set; }
    public Country Departure { get; set; }
    public Country Destination { get; set; }
    public Airport ArrivalAirport { get; set; }
    public DateTime DepartureDate { get; set; }
    public int AvailableSeats { get; set; }
}