using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Models;

public class FlightClassInfo
{
    public FlightClass ClassType { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }

    public FlightClassInfo(FlightClass classType, int availableSeats, decimal price)
    {
        this.ClassType = classType;
        this.AvailableSeats = availableSeats;
        this.Price = price;
    }
    
    public override string ToString()
    {
        return $"Class: {ClassType}, Seats Available: {AvailableSeats}, Price: {Price:C}";
    }
}