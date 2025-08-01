using System.Globalization;
using FTSAirportTicketBookingSystem.Models.DTOs;
using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Models.Flight;

public static class FlightMapper
{
    public static Flight FromCsvDto(FlightCsvDto dto)
    {
        return new Flight
        {
            Id = Guid.Parse(dto.Id),
            BasePrice = dto.BasePrice,
            DepartureDate = DateTime.Parse(dto.DepartureDate),
            Departure = new Country { Id = Guid.NewGuid(), Name = dto.DepartureCountry },
            Destination = new Country {Id = Guid.NewGuid(), Name = dto.DestinationCountry },
            DepartureAirport = new Airport { Id = Guid.NewGuid(), Name = dto.DepartureAirport, Country = new Country { Id = Guid.NewGuid(), Name = dto.DepartureAirport } },
            ArrivalAirport = new Airport { Id = Guid.NewGuid(), Name = dto.ArrivalAirport, Country = new Country { Id = Guid.NewGuid(), Name = dto.ArrivalAirport } },
            AvailableClasses = ParseFlightClasses(dto.AvailableClasses)
        };
    }
    
    public static List<FlightClassInfo> ParseFlightClasses(string classesString)
    {
        if (string.IsNullOrWhiteSpace(classesString))
            return new List<FlightClassInfo>();

        return classesString.Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(classEntry =>
            {
                var parts = classEntry.Split(':');
                if (parts.Length != 3)
                    throw new FormatException($"Invalid flight class format: '{classEntry}'");

                var classType = Enum.Parse<FlightClass>(parts[0]);
                var availableSeats = int.Parse(parts[1]);
                var price = decimal.Parse(parts[2], CultureInfo.InvariantCulture);

                return new FlightClassInfo(classType, availableSeats, price);
            })
            .ToList();
    }
}
