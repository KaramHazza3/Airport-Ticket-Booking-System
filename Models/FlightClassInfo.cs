using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Models;

public class FlightClassInfo
{
    public FlightClass ClassType { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }
}