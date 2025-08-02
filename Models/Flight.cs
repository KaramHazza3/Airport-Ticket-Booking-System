using System.ComponentModel.DataAnnotations;
using FTSAirportTicketBookingSystem.Common.CustomAttributes;
using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Models;

public class Flight
{
    [Required]
    public Guid Id { get; set; }
    public decimal BasePrice { get; set; }
    [Required]
    public Country Departure { get; set; }
    [Required]
    public Country Destination { get; set; }
    [Required]
    public Airport DepartureAirport { get; set; }
    [Required]
    public Airport ArrivalAirport { get; set; }
    [Required]
    [Future]
    public DateTime DepartureDate { get; set; }
    [Required]
    public List<FlightClassInfo> AvailableClasses { get; init; }
    public int TotalAvailableSeats => AvailableClasses.Sum(c => c.AvailableSeats);

    public Flight()
    {
        
    }
    public Flight(Country departure,
        Country destination, Airport departureAirport,
        Airport arrivalAirport, DateTime departureDate,
        List<FlightClassInfo> availableClasses)
    {
        this.Id = Guid.NewGuid(); 
        this.Departure = departure;
        this.Destination = destination;
        this.DepartureAirport = departureAirport;
        this.ArrivalAirport = arrivalAirport;
        this.DepartureDate = departureDate;
        this.AvailableClasses = availableClasses;
        this.BasePrice = AvailableClasses!.Where(ac => ac.ClassType == FlightClass.Economy).Sum(x => x.Price);
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