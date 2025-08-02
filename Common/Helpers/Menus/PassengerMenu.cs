using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Models.Enums;
using FTSAirportTicketBookingSystem.Services.BookingService;
using FTSAirportTicketBookingSystem.Services.FlightService;

namespace FTSAirportTicketBookingSystem.Common.Helpers.Menus;

public class PassengerMenu
{
    private readonly IBookingService _bookingService;
    private readonly IFlightService _flightService;

    public PassengerMenu(IBookingService bookingService, IFlightService flightService)
    {
        this._bookingService = bookingService;
        this._flightService = flightService;
    }
    
    private static void Show()
    {
        Console.WriteLine("1. Search for available flights");
        Console.WriteLine("2. Book a flight");
        Console.WriteLine("3. Cancel a flight");
        Console.WriteLine("4. View my bookings");
        Console.WriteLine("5. Edit my booking");
        Console.WriteLine("6. Exit");
    }
    
    public static void ShowFlightClassesMenu()
    {
        Console.WriteLine("Select Class");
        Console.WriteLine("1. Economy");
        Console.WriteLine("2. Business");
        Console.WriteLine("3. First Class");
    }

    public static void ShowSearchParameters()
    {
        Console.WriteLine("1. By Price");
        Console.WriteLine("2. By Departure Country");
        Console.WriteLine("3. By Destination Country");
        Console.WriteLine("4. By Departure Airport");
        Console.WriteLine("5. By Arrival Airport");
        Console.WriteLine("6. By Class");
        Console.WriteLine("7. Exit");
    }
    public async Task Handle(User user)
    {
        while (true)
        {
            Show();
            var passengerInput = Console.ReadLine();

            switch (passengerInput)
            {
                case "1":
                    await SearchForAvailableFlights();
                    break;
                case "2":
                    await BookFlight(user);
                    break;
                case "3":
                    await CancelBooking(user);
                    break;
                case "4":
                    await ViewBookings(user);
                    break;
                case "5":
                    await EditBooking(user);
                    break;
                case "6":
                    AppMenu.Exit();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private async Task SearchForAvailableFlights()
    {
        while (true)
        {
            ShowSearchParameters();
            var passengerSearchInput = Console.ReadLine();
            switch (passengerSearchInput)
            {
                case "1":
                    Console.Write("Enter the price: ");
                    if (!decimal.TryParse(Console.ReadLine(), out var price))
                    {
                        Console.WriteLine("Invalid input");
                        return;
                    }
                    await SearchFlights(f => f.BasePrice == price);
                    break;
                case "2":
                    Console.Write("Enter the country: ");
                    var departureCountryName = Console.ReadLine();
                    if (string.IsNullOrEmpty(departureCountryName))
                    {
                        Console.WriteLine("Invalid input");
                        return;
                    }
                    await SearchFlights(f => f.Departure.Name == departureCountryName);
                    break;
                case "3":
                    Console.Write("Enter the country: ");
                    var destinationCountryName = Console.ReadLine();
                    if (string.IsNullOrEmpty(destinationCountryName))
                    {
                        Console.WriteLine("Invalid input");
                        return;
                    }
                    await SearchFlights(f => f.Destination.Name == destinationCountryName);
                    break;
                case "4":
                    Console.Write("Enter the airport: ");
                    var departureAirportName = Console.ReadLine();
                    if (string.IsNullOrEmpty(departureAirportName))
                    {
                        Console.WriteLine("Invalid input");
                        return;
                    }
                    await SearchFlights(f => f.DepartureAirport.Name == departureAirportName);
                    break;
                case "5":
                    Console.Write("Enter the airport: ");
                    var arrivalAirportName = Console.ReadLine();
                    if (string.IsNullOrEmpty(arrivalAirportName))
                    {
                        Console.WriteLine("Invalid input");
                        return;
                    }
                    await SearchFlights(f => f.ArrivalAirport.Name == arrivalAirportName);
                    break;
                case "6":
                    Console.Write("Enter the class: ");
                    var className = Console.ReadLine();
                    if (string.IsNullOrEmpty(className))
                    {
                        Console.WriteLine("Invalid input");
                        return;
                    }
                    await SearchFlights(f => f.AvailableClasses.Exists(ac => ac.ClassType.ToString() == className));
                    break;
                case "7":
                    AppMenu.Exit();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

        }
    }
    private async Task SearchFlights(Func<Flight, bool> predicate)
    {
        var result = await _flightService.FilterAsync(predicate);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error.Description);
            return;
        }

        var flights = result.Value;
        if (!flights.Any())
        {
            Console.WriteLine("No flights found matching your criteria.");
            return;
        }

        DisplayFlights(flights);
    }
    
    private void DisplayFlights(List<Flight> flights)
    {
        foreach (var flight in flights)
        {
            Console.WriteLine($"Flight Id: {flight.Id}");
            Console.WriteLine($"From: {flight.Departure.Name}, Airport: {flight.DepartureAirport.Name}");
            Console.WriteLine($"To: {flight.Destination.Name}, Airport: {flight.ArrivalAirport.Name}");
            Console.WriteLine($"Base price: {flight.BasePrice}");
            Console.WriteLine("Available Classes: " +
                              string.Join(" | ", flight.AvailableClasses.Select(ac =>
                                  $"{ac.ClassType} (Seats: {ac.AvailableSeats}, Price: {ac.Price:C})")));
            Console.WriteLine(new string('-', 40));
        }
    }

    private async Task EditBooking(User user)
    {
        var myBookingsResult = await this._bookingService.FilterAsync(b => b.Passenger.Id == user.Id);
        if (myBookingsResult.IsFailure)
        {
            Console.WriteLine(myBookingsResult.Error.Description);
            return;
        }

        var myBookings = myBookingsResult.Value;
        Console.WriteLine("Edit my booking");
        Console.Write("Enter booking id: ");

        if (!Guid.TryParse(Console.ReadLine(), out var bookingId) || !myBookings.Exists(b => b.Id == bookingId))
        {
            Console.WriteLine("Invalid booking id");
            return;
        }

        var bookingToUpdate = myBookings.SingleOrDefault(b => b.Id == bookingId);
        ShowUpdateBookingParameters();
        Console.Write("Enter what you want to update: ");
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                ShowFlightClassesMenu();
                var flightClassinput = Console.ReadLine();
                switch (flightClassinput)
                {
                    case "1":
                        bookingToUpdate!.FlightClass = FlightClass.Economy;
                        break;
                    case "2":
                        bookingToUpdate!.FlightClass = FlightClass.Business;
                        break;
                    case "3":
                        if (!bookingToUpdate!.Flight.AvailableClasses.Exists(ac =>
                                ac.ClassType == FlightClass.FirstClass))
                        {
                            Console.WriteLine("The flight doesn't have first class");
                            return;
                        }
                        bookingToUpdate!.FlightClass = FlightClass.FirstClass;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        return;
                }

                break;
            case "2":
                var flightsResult = await GetAllAvailableFlights();
                if (flightsResult.IsFailure)
                {
                    Console.WriteLine("There is an issue while getting available flights");
                    return;
                }

                var flights = flightsResult.Value;
                DisplayFlights(flights);
                Console.Write("Enter the flight id: ");
                var flightIdInput = Console.ReadLine();
                if (!Guid.TryParse(flightIdInput, out var flightId) &&
                    !flights.Exists(f => f.Id == flightId))
                {
                    Console.WriteLine("Invalid input");
                    return;
                }

                var flight = flights.SingleOrDefault(f => f.Id == flightId);
                Console.Write("Choose the class: ");
                bookingToUpdate!.Flight = flight!;
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
        var updateResult = await _bookingService.UpdateAsync(bookingId, bookingToUpdate!);

        if (updateResult.IsFailure)
        {
            Console.WriteLine(updateResult.Error);
            return;
        }

        Console.WriteLine("Booking updated successfully");
    }
    
    public static void ShowUpdateBookingParameters()
    {
        Console.WriteLine("1. Flight Class");
        Console.WriteLine("2. Flight");
    }

    private async Task<Result<List<Flight>>> GetAllAvailableFlights()
    {
        var flightsResult = await this._flightService.GetAllAsync();
        if (flightsResult.IsFailure)
        {
            Console.WriteLine(flightsResult.Error);
            return new List<Flight>();
        }

        return flightsResult.Value.ToList();
    }
    private async Task BookFlight(User user)
    {
        Console.WriteLine("Book a flight");

        var flightsResult = await this._flightService.GetAllAsync();
        if (flightsResult.IsFailure)
        {
            Console.WriteLine(flightsResult.Error.Description);
            return;
        }

        var flights = flightsResult.Value.ToList();
        DisplayFlights(flights);
        
        Console.Write("Enter flight id: ");
        if (!Guid.TryParse(Console.ReadLine(), out var flightId) || !flights.Exists(f => f.Id == flightId))
        {
            Console.WriteLine("Invalid flight id");
            return;
        }

        ShowFlightClassesMenu(); 

        if (!int.TryParse(Console.ReadLine(), out var classInput) || classInput < 1 || classInput > 3)
        {
            Console.WriteLine("Invalid class selection");
            return;
        }

        var flightClass = classInput switch
        {
            1 => FlightClass.Economy,
            2 => FlightClass.Business,
            3 => FlightClass.FirstClass,
            _ => FlightClass.Economy
        };
        var flight = flights.SingleOrDefault(f => f.Id == flightId);
        if (!flight!.AvailableClasses.Exists(ac => ac.ClassType == FlightClass.FirstClass))
        {
            Console.WriteLine("The flight doesn't contain this class");
            return;
        }
        var booking = new Booking(user, flight!, flightClass);
        var bookingResult = await _bookingService.AddAsync(booking);

        if (bookingResult.IsFailure)
        {
            Console.WriteLine(bookingResult.Error);
            return;
        }

        Console.WriteLine("Booking successful");
    }
    
    private async Task CancelBooking(User user)
    {
        Console.WriteLine("Cancel my booking");

        var myBookingsResult = await this._bookingService.FilterAsync(b => b.Passenger.Id == user.Id);
        if (myBookingsResult.IsFailure)
        {
            Console.WriteLine(myBookingsResult.Error.Description);
            return;
        }

        var myBookings = myBookingsResult.Value;

        Console.Write("Enter booking id: ");

        if (!Guid.TryParse(Console.ReadLine(), out var bookingId) || !myBookings.Exists(b => b.Id == bookingId))
        {
            Console.WriteLine("Invalid booking id");
            return;
        }

        var bookingToDelete = myBookings.SingleOrDefault(b => b.Id == bookingId);
        var deleteResult = await _bookingService.DeleteAsync(bookingToDelete!.Id);

        if (deleteResult.IsFailure)
        {
            Console.WriteLine(deleteResult.Error);
            return;
        }

        Console.WriteLine("Booking cancelled");
    }
    
    private async Task ViewBookings(User user)
    {
        Console.WriteLine("View my bookings");
        var myBookingsResult = await this._bookingService.FilterAsync(b => b.Passenger.Id == user.Id);
        if (myBookingsResult.IsFailure)
        {
            Console.WriteLine(myBookingsResult.Error.Description);
            return;
        }

        var myBookings = myBookingsResult.Value;
        foreach (var b in myBookings)
        {
            Console.WriteLine($"Booking Id: {b.Id}");
            Console.WriteLine($"Flight Id: {b.Flight.Id}");
            Console.WriteLine($"Class: {b.FlightClass}");
            Console.WriteLine($"Passenger Id: {b.Passenger.Id}");
            Console.WriteLine("====================================");
        }
    }
}