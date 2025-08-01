using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FTSAirportTicketBookingSystem.Models;

public class Country
{
    public  Guid Id { get; set; }
    [Required]
    public  string Name { get; set; }
    public string Code { get; set; } = string.Empty;
    
    public Country() {}
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