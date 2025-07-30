namespace FTSAirportTicketBookingSystem.Models;

public class Flight
{
    public Guid Id { get; set; }
    public decimal BasePrice { get; set; }
    public Country Departure { get; set; }
    public Country Destination { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
    public DateTime DepartureDate { get; set; }
    public List<FlightClassInfo> AvailableClasses { get; init; }
    public int TotalAvailableSeats => AvailableClasses.Sum(c => c.AvailableSeats);

    public Flight(decimal basePrice, Country departure,
        Country destination, Airport departureAirport,
        Airport arrivalAirport, DateTime departureDate,
        List<FlightClassInfo> flightClasses)
    {
        this.Id = Guid.NewGuid(); 
        this.BasePrice = basePrice;
        this.Departure = departure;
        this.Destination = destination;
        this.DepartureAirport = departureAirport;
        this.ArrivalAirport = arrivalAirport;
        this.DepartureDate = departureDate;
        this.AvailableClasses = flightClasses;
    }
    
    public override string ToString()
    {
        var classInfo = string.Join(", ",
            AvailableClasses.Select(c =>
                $"{c.ClassType} (Seats: {c.AvailableSeats}, Price: {c.Price})"));

        return $"Flight ID: {Id}\n" +
               $"Base Price: {BasePrice:C}\n" +
               $"From: {Departure} - {DepartureAirport}\n" +
               $"To: {Destination} - {ArrivalAirport}\n" +
               $"Departure Date: {DepartureDate:yyyy-MM-dd HH:mm}\n" +
               $"Total Available Seats: {TotalAvailableSeats}\n" +
               $"Available Classes: {classInfo}";
    }

}