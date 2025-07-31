namespace FTSAirportTicketBookingSystem.Models;

public class Country
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string Code { get; set; } = String.Empty;
    
    public override string ToString()
    {
        return $"{Name} ({Code}) - Id: {Id}";
    }
}