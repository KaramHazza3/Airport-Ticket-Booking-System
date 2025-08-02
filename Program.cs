using FTSAirportTicketBookingSystem.Common.Helpers.Menus;
using FTSAirportTicketBookingSystem.Repository;
using FTSAirportTicketBookingSystem.Services.AuthService;
using FTSAirportTicketBookingSystem.Services.BookingService;
using FTSAirportTicketBookingSystem.Services.FlightService;
using FTSAirportTicketBookingSystem.Services.ImportFileService;
using FTSAirportTicketBookingSystem.Services.UserService;

namespace FTSAirportTicketBookingSystem;

class Program
{
    static void Main(string[] args)
    {
        IRepository fileRepo = FileRepository.Instance;
        var importService = new ImportCsvFileService();
        var userService = new UserService(fileRepo);
        var flightService = new FlightService(fileRepo);
        var bookingService = new BookingService(fileRepo);
        var authService = new AuthService(userService);
        var appMenu = new AppMenu(authService, bookingService, flightService, importService);
        appMenu.Handle().Wait();
    }
}
