using System.Diagnostics.CodeAnalysis;

namespace FTSAirportTicketBookingSystem.Models;

public class Airport
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Country Country { get; set; }

    public Airport() {}
    [SetsRequiredMembers]
    public Airport(string name, Country country)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Country = country;
    }
    
    public override string ToString()
    {
        return $"Airport: {Name} - Id: {Id}";
    }
}