using FTSAirportTicketBookingSystem.Models.Enums;
namespace FTSAirportTicketBookingSystem.Models;

public class Booking : IEquatable<Booking>
{
    public Guid Id { get; init; }
    public User Passenger { get; set; }
    public Flight Flight { get; set; }
    public FlightClass FlightClass { get; set; }
    public DateTime BookingDate { get; }
    public int FlightTime { get; set; }
    
    public decimal Price { get; set; }

    public Booking()
    {
        this.Id = Guid.NewGuid();
    }
    public Booking(User passenger, Flight flight, FlightClass flightClass, int flightTime)
    {
        this.Id = Guid.NewGuid();
        this.Passenger = passenger;
        this.Flight = flight;
        this.FlightClass = flightClass;
        this.BookingDate = DateTime.UtcNow;
        this.FlightTime = flightTime;
        this.Price = this.Flight.AvailableClasses
            .Where(x => x.ClassType == flightClass)
            .Sum(x => x.Price);
    }

    public override bool Equals(object? obj) => Equals(obj as Booking);

    public bool Equals(Booking? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }
    public override int GetHashCode() => Id.GetHashCode();
    
    public override string ToString()
    { 
        return $@"Booking {{
    Id           : {Id}
    PassengerName  : {Passenger.Name}
    FlightId    : {Flight.Id}
    FlightClass  : {FlightClass}
    BookingDate  : {BookingDate:yyyy-MM-dd HH:mm:ss} (UTC)
    FlightTime   : {FlightTime} hours
}}";
    }
}