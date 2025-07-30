namespace FTSAirportTicketBookingSystem.Models;

public record Airport(Guid Id, string Name, Country Country)
{
    public Airport(string name, Country country): this(Guid.NewGuid(), name, country) { }
}