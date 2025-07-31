namespace FTSAirportTicketBookingSystem.Models;

public class Airport
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Country Country { get; set; }
    
    public override string ToString()
    {
        return $"Airport: {Name} - Id: {Id}";
    }
}