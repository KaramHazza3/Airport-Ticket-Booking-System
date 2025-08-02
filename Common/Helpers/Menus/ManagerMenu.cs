using FTSAirportTicketBookingSystem.Common.Validators.CsvValidators.FlightValidator;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Models.DTOs;
using FTSAirportTicketBookingSystem.Models.Mappers;
using FTSAirportTicketBookingSystem.Services.BookingService;
using FTSAirportTicketBookingSystem.Services.FlightService;
using FTSAirportTicketBookingSystem.Services.ImportFileService;

namespace FTSAirportTicketBookingSystem.Common.Helpers.Menus;

public class ManagerMenu
{
    private readonly IBookingService _bookingService;
    private readonly IFileImportService _importService;
    private readonly IFlightService _flightService;


    public ManagerMenu(IBookingService bookingService, IFileImportService importService, IFlightService flightService)
    {
        this._bookingService = bookingService;
        this._importService = importService;
        this._flightService = flightService;
    }
    private static void ShowMenu()
    {
        Console.WriteLine("1. Import from CSV");
        Console.WriteLine("2. View Validations Rules");
        Console.WriteLine("3. Filter Bookings");
        Console.WriteLine("4. Exit");
    }

    private static void ShowFilterBookingMenu()
    {
        Console.WriteLine("Filter Bookings");
        Console.WriteLine("1. By Flight");
        Console.WriteLine("2. By Passenger");
        Console.WriteLine("3. By Departure Country");
        Console.WriteLine("4. By Destination Country");
        Console.WriteLine("5. By Departure Airport");
        Console.WriteLine("6. By Destination Airport");
        Console.WriteLine("7. By Flight Class Type");
        Console.WriteLine("8. Exit");
    }
    
    private static void ShowAvailableClassesMenu()
    {
        Console.WriteLine("Validation rules of available classes");
        Console.WriteLine("1. User");
        Console.WriteLine("2. Flight");
        Console.WriteLine("3. Booking");
    }
    
    public async Task Handle()
    {
        while (true)
        {
            ShowMenu();
            var managerInput = Console.ReadLine();

            switch (managerInput)
            {
                case "1":
                    await ImportFlightsFromCSV();
                    break;
                case "2":
                    GetSpecificClassConstraints();
                    break;
                case "3":
                    await FilterBookingFiltering();
                    break;
                case "4":
                    AppMenu.Exit();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private void GetSpecificClassConstraints()
    {
        ShowAvailableClassesMenu();
        Console.Write("Enter the class name: ");
        var className = Console.ReadLine();
        if (string.IsNullOrEmpty(className))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        switch (className)
        {
            case "1":
                foreach (var attributeConstraint in AttributeConstraintsGenerator.GetAttributesConstraints<User>())
                {
                    Console.WriteLine(attributeConstraint);
                }
                break;
            case "2":
                foreach (var attributeConstraint in AttributeConstraintsGenerator.GetAttributesConstraints<Flight>())
                {
                    Console.WriteLine(attributeConstraint);
                }
                break;
            case "3":
                foreach (var attributeConstraint in AttributeConstraintsGenerator.GetAttributesConstraints<Booking>())
                {
                    Console.WriteLine(attributeConstraint);
                }
                break;
            case "4":
                AppMenu.Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private async Task FilterBookingFiltering()
    {
      
        ShowFilterBookingMenu();
        Console.WriteLine("Enter filter strategy: ");
        var filterInput = Console.ReadLine();
        if (string.IsNullOrEmpty(filterInput))
        {
            Console.WriteLine("Invalid input");
            return;
        }
       
        switch (filterInput)
        {
            case "1":
                Console.Write("Enter flight id: ");
                if (!Guid.TryParse(Console.ReadLine(), out var flightId))
                {
                    Console.WriteLine("Invalid input");
                    return;
                }
                await FilterBooking(b => b.Flight.Id == flightId);
                break;
            case "2":
                Console.Write("Enter user id: ");
                if (!Guid.TryParse(Console.ReadLine(), out var userId))
                {
                    Console.WriteLine("Invalid input");
                    return;
                }
                await FilterBooking(b => b.Passenger.Id == userId);
                break;
            case "3":
                Console.Write("Enter country name: ");
                var departureCountry = Console.ReadLine();
                if (string.IsNullOrEmpty(departureCountry))
                {
                    Console.WriteLine("Invalid Input");
                    return;
                }
                await FilterBooking(b => b.Flight.Departure.Name == departureCountry);
                break;
            case "4":
                Console.Write("Enter the country name: ");
                var destinationCountry = Console.ReadLine();
                if (string.IsNullOrEmpty(destinationCountry))
                {
                    Console.WriteLine("Invalid Input");
                    return;
                }
                await FilterBooking(b => b.Flight.Destination.Name == destinationCountry);
                break;
            case "5":
                Console.Write("Enter the airport name: ");
                var departureAirport = Console.ReadLine();
                if (string.IsNullOrEmpty(departureAirport))
                {
                    Console.WriteLine("Invalid Input");
                    return;
                }
                await FilterBooking(b => b.Flight.DepartureAirport.Name == departureAirport);
                break;
            case "6":
                Console.Write("Enter the airport name: ");
                var destinationAirport = Console.ReadLine();
                if (string.IsNullOrEmpty(destinationAirport))
                {
                    Console.WriteLine("Invalid Input");
                    return;
                }
                await FilterBooking(b => b.Flight.ArrivalAirport.Name == destinationAirport);
                break;
            case "7":
                Console.Write("Enter the flight class name: ");
                var flightClassNameInput = Console.ReadLine();
                if (string.IsNullOrEmpty(flightClassNameInput))
                {
                    Console.WriteLine("Invalid Input");
                    return;
                }
                await FilterBooking(b => b.FlightClass.ToString() == flightClassNameInput);
                break;
            case "8":
                AppMenu.Exit();
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }

    private async Task ImportFlightsFromCSV()
    {
        Console.WriteLine("Import from CSV");
        Console.Write("Enter your file path: ");
        Console.WriteLine("Note: you can take a look for sample file in the Project folder");


        var filePath = Console.ReadLine();
        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("Invalid input");
            return;
        }
        var csvResult = await _importService.ImportFileAsync<Flight, FlightCsvDto>(filePath, FlightMapper.FromCsvDto,
            _flightService, FlightCsvValidator.Validate);

        if (csvResult.IsFailure)
        {
            Console.WriteLine(csvResult.Error);
            return;
        }

        Console.WriteLine("Import successful");
    }
    
    private async Task FilterBooking(Func<Booking, bool> predicate)
    {
        var result = await _bookingService.FilterAsync(predicate);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error.Description);
            return;
        }

        var bookings = result.Value;
        if (!bookings.Any())
        {
            Console.WriteLine("No flights found matching your criteria.");
            return;
        }

        foreach (var booking in bookings)
        {
            Console.WriteLine(booking);
        }
    }
}