using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Models;

public class Booking
{
    public Guid Id { get; set; }
    public string PassengerId { get; init; }
    public string FlightId { get; init; }
    public FlightClass FlightClass { get; init; }
    public DateTime BookingDate { get; init; }
    public int FlightTime { get; set; }

    public Booking()
    {
        this.Id = Guid.NewGuid();
    }
    public Booking(string passengerId, string flightId, FlightClass flightClass, int flightTime)
    {
        this.Id = Guid.NewGuid();
        this.PassengerId = passengerId;
        this.FlightId = flightId;
        this.FlightClass = flightClass;
        this.BookingDate = DateTime.UtcNow;
        this.FlightTime = flightTime;
    }
}