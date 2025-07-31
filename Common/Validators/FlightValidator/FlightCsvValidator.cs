using System.Globalization;
using FTSAirportTicketBookingSystem.Models.DTOs;
using FTSAirportTicketBookingSystem.Models.Enums;

namespace FTSAirportTicketBookingSystem.Common.Validators.FlightValidator;

public static class FlightCsvValidator
{
    public static List<ValidationError> Validate(FlightCsvDto dto, int rowNumber)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(Convert.ToString(dto.Id)) || !Guid.TryParse(Convert.ToString(dto.Id), out _))
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.Id),
                ErrorMessage = "Invalid or missing Id."
            });
        }

        if (dto.BasePrice <= 0)
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.BasePrice),
                ErrorMessage = "BasePrice must be greater than zero."
            });
        }

        if (string.IsNullOrWhiteSpace(dto.DepartureCountry))
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.DepartureCountry),
                ErrorMessage = "DepartureCountry is required."
            });
        }

        if (string.IsNullOrWhiteSpace(dto.DepartureAirport))
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.DepartureAirport),
                ErrorMessage = "DepartureAirport is required."
            });
        }

        if (string.IsNullOrWhiteSpace(dto.DestinationCountry))
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.DestinationCountry),
                ErrorMessage = "DestinationCountry is required."
            });
        }

        if (string.IsNullOrWhiteSpace(dto.ArrivalAirport))
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.ArrivalAirport),
                ErrorMessage = "ArrivalAirport is required."
            });
        }

        if (!DateTime.TryParseExact(dto.DepartureDate, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var departureDate))
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.DepartureDate),
                ErrorMessage = "Invalid DepartureDate format."
            });
        }
        else if (departureDate < DateTime.Now)
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.DepartureDate),
                ErrorMessage = "DepartureDate cannot be in the past."
            });
        }

        if (string.IsNullOrWhiteSpace(dto.AvailableClasses))
        {
            errors.Add(new ValidationError
            {
                RowNumber = rowNumber,
                PropertyName = nameof(dto.AvailableClasses),
                ErrorMessage = "AvailableClasses is required."
            });
        }
        else
        {
            var classEntries = dto.AvailableClasses.Split(';');
            foreach (var classEntry in classEntries)
            {
                var parts = classEntry.Split(':');
                if (parts.Length != 3)
                {
                    errors.Add(new ValidationError
                    {
                        RowNumber = rowNumber,
                        PropertyName = nameof(dto.AvailableClasses),
                        ErrorMessage = $"Invalid class entry format: '{classEntry}'. Expected format: ClassType:Seats:Price"
                    });
                    continue;
                }

                if (!Enum.TryParse<FlightClass>(parts[0], out _))
                {
                    errors.Add(new ValidationError
                    {
                        RowNumber = rowNumber,
                        PropertyName = nameof(dto.AvailableClasses),
                        ErrorMessage = $"Invalid class type: '{parts[0]}'."
                    });
                }

                if (!int.TryParse(parts[1], out int seats) || seats < 0)
                {
                    errors.Add(new ValidationError
                    {
                        RowNumber = rowNumber,
                        PropertyName = nameof(dto.AvailableClasses),
                        ErrorMessage = $"Invalid seat count: '{parts[1]}'. Must be a non-negative integer."
                    });
                }

                if (!decimal.TryParse(parts[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price) || price <= 0)
                {
                    errors.Add(new ValidationError
                    {
                        RowNumber = rowNumber,
                        PropertyName = nameof(dto.AvailableClasses),
                        ErrorMessage = $"Invalid price: '{parts[2]}'. Must be a positive decimal."
                    });
                }
            }
        }

        return errors;
    }
}