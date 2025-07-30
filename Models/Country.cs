namespace FTSAirportTicketBookingSystem.Models;

public record Country(Guid Id, string Name, string Code)
{
    public Country(string name, string code) : this(Guid.NewGuid(), name, code) {}
}