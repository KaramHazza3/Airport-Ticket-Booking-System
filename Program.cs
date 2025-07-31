using CsvHelper;
using FTSAirportTicketBookingSystem.Common.Validators;
using FTSAirportTicketBookingSystem.Common.Validators.FlightValidator;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Models.DTOs;
using FTSAirportTicketBookingSystem.Models.Enums;
using FTSAirportTicketBookingSystem.Models.Flight;
using FTSAirportTicketBookingSystem.Repository;
using FTSAirportTicketBookingSystem.Services.AuthService;
using FTSAirportTicketBookingSystem.Services.FlightService;
using FTSAirportTicketBookingSystem.Services.ImportFileService;
using FTSAirportTicketBookingSystem.Services.UserService;

namespace FTSAirportTicketBookingSystem;

class Program
{
    static async Task Main(string[] args)
    {
        // IRepository fileRepo = FileRepository.Instance;
        // var userService = new UserService(fileRepo);
        // var authService = new AuthService(userService); 
        // var passengerService = new PassengerService(fileRepo);
        // var managerService = new ManagerService(fileRepo);
        // var country1 = new Country("United States", "US");
        // var country2 = new Country("United Kingdom", "UK");
        // var country3 = new Country("Palestine Authority", "PS");
        // var airport1 = new Airport("USA Airport", country1);
        // var airport2 = new Airport("UK Airport", country2);
        // var airport3 = new Airport("Palestine Airport", country3);
        // var flightClass1 = new FlightClassInfo(FlightClass.Business, 50, 50);
        // var flightClass2 = new FlightClassInfo(FlightClass.FirstClass, 20, 100);
        // var flightClass3 = new FlightClassInfo(FlightClass.Economy, 150, 10);
        // var flightClasses = new List<FlightClassInfo> {flightClass1, flightClass2, flightClass3};
        // var flight1 = new Flight(30m, country1, country2, airport1, airport2, DateTime.UtcNow, flightClasses);
        // var flight2 = new Flight(30m, country2, country3, airport2, airport1, DateTime.Now, flightClasses);
        // await fileRepo.WriteAsync(country1);
        // await fileRepo.WriteAsync(country2);
        // await fileRepo.WriteAsync(country3);
        // await fileRepo.WriteAsync(airport1);
        // await fileRepo.WriteAsync(airport2);
        // await fileRepo.WriteAsync(airport3);
        // await fileRepo.WriteAsync(flightClass1);
        // await fileRepo.WriteAsync(flightClass2);
        // await fileRepo.WriteAsync(flightClass3);
        // await fileRepo.WriteAsync(flight1);
        // await fileRepo.WriteAsync(flight2);
        
        // var newUser = new User()
        // {
        //     Name = "Amr",
        //     Email = "Amr@gmail.com",
        //     Password = "Amr",
        //     Role = UserRole.Passenger
        // };
        // var result1 = await authService.Register(newUser.Name, newUser.Email, newUser.Password, newUser.Role);
        // if (result1.IsFailure)
        // {
        //     Console.WriteLine(result1.Error.Description);
        // }
        // var result2 = await authService.Login("Amr@gmail.com", "Amr");
        // if (result2.IsFailure)
        // {
        //     Console.WriteLine(result2.Error.Description);
        //     return;
        // }
        // var user = result2.Value;
        // Console.WriteLine($"{user} is logged in");
        //
        // var booking1 = new Booking(user.Id, flight1.Id, flightClass1, 2);
        // var returnedBooking = await passengerService.BookFlight(booking1);
        // if (returnedBooking.IsFailure)
        // {
        //     Console.WriteLine(returnedBooking.Error.Description);
        //     return;
        // }
        // Console.WriteLine(returnedBooking.Value);
        // var resultOf = await passengerService.GetPersonalBookings(user.Id);
        // if (resultOf.IsFailure)
        // {
        //     return;
        // }
        //
        // var myPersonalBookings = resultOf.Value;
        // foreach (var booking in myPersonalBookings)
        // {
        //     Console.WriteLine(booking);
        // }
        
        // var result = await passengerService.CancelBooking(new Guid("824d87a2-96c2-4cd0-88ff-e488a463cc17"));
        // if (result.IsFailure)
        // {
        //     Console.WriteLine(result.Error.Description);
        //     return;
        // }
        //
        // Console.WriteLine("The booking has been deleted successfully");
        
        // var result = await managerService.FilterAsync(b => b. == "US",
        //                                                 b => b.BasePrice == 30);
        // if (result.IsFailure)
        // {
        //     Console.WriteLine(result.Error.Description);
        //     return;
        // }
        //
        // foreach (var booking in result.Value)
        // {
        //     Console.WriteLine(booking);
        // }
        var importService = new ImportCsvFileService<Flight, FlightCsvDto>();
        IRepository repo = FileRepository.Instance;
        var flightService = new FlightService(repo);
        var result = await importService.ImportFileAsync(
            "C:\\Users\\karam\\Desktop\\FTS\\FTSAirportTicketBookingSystem\\sample.csv",
            FlightMapper.FromCsvDto,
            flightService,
            FlightCsvValidator.Validate);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error.Description);
        }

        foreach (var flight in result.Value)
        {
            Console.WriteLine(flight);
        }
    }
}
