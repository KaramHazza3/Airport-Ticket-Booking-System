namespace FTSAirportTicketBookingSystem.Models;

public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    public Country(string name, string code)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Code = code;
    }
}