using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Repository;
using FTSAirportTicketBookingSystem.Services.AuthService;
using FTSAirportTicketBookingSystem.Services.UserService;

namespace FTSAirportTicketBookingSystem;

class Program
{
    static async Task Main(string[] args)
    {
        var fileRepo = FileRepository.Instance;
        var userService = new UserService(fileRepo);
        var authService = new AuthService(userService);
        var newUser = new User()
        {
            Name = "Karam",
            Email = "Karam@gmail.com",
            Password = "Karam123",
            Role = UserRole.Passenger
        };
        var result = await authService.Register(newUser.Name, newUser.Email, newUser.Password, newUser.Role);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error.Description);
        }
        var result2 = await authService.Login("Karam@gmail.com", "Karam123");
        if (result2.IsFailure)
        {
            Console.WriteLine(result2.Error.Description);
            return;
        }

        var user = result2.Value;
        Console.WriteLine(user);
    }
}