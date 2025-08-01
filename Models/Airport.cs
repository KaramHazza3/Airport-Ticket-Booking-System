using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FTSAirportTicketBookingSystem.Models;

public class Airport
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public Country Country { get; set; }

    public Airport()
    {
        this.Id = Guid.NewGuid();
    }
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