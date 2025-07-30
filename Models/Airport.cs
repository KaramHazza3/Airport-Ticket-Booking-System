namespace FTSAirportTicketBookingSystem.Models;

public class Airport
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }

    public Airport(string name, Country country)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Country = country;
    }
}