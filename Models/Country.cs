using System.Diagnostics.CodeAnalysis;

namespace FTSAirportTicketBookingSystem.Models;

public class Country
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string Code { get; set; } = String.Empty;
    
    public Country() {}
    [SetsRequiredMembers]
    public Country(string name, string code)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Code = code;
    }
    public override string ToString()
    {
        return $"{Name} ({Code}) - Id: {Id}";
    }
}