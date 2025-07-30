using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Models;

public class Booking : IEquatable<Booking>
{
    public Guid Id { get; init; }
    public Guid PassengerId { get; set; }
    public Guid FlightId { get; set; }
    public FlightClass FlightClass { get; set; }
    public DateTime BookingDate { get; }
    public int FlightTime { get; set; }
    
    public decimal Price { get; set; }

    public Booking()
    {
        this.Id = Guid.NewGuid();
    }
    public Booking(Guid passengerId, Guid flightId, FlightClass flightClass, int flightTime, decimal price)
    {
        this.Id = Guid.NewGuid();
        this.PassengerId = passengerId;
        this.FlightId = flightId;
        this.FlightClass = flightClass;
        this.BookingDate = DateTime.UtcNow;
        this.FlightTime = flightTime;
        this.Price = price;
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
    PassengerId  : {PassengerId}
    FlightId     : {FlightId}
    FlightClass  : {FlightClass}
    BookingDate  : {BookingDate:yyyy-MM-dd HH:mm:ss} (UTC)
    FlightTime   : {FlightTime} hours
}}";
    }
}