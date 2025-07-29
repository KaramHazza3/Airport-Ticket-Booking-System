using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Errors;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Services.UserService;

namespace FTSAirportTicketBookingSystem.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        this._userService = userService;
    }
    
    public async Task<Result> Register(string name, string email, string password, UserRole role)
    {
        var newUser = new User(name, email, password, role);
        return await this._userService.AddUserAsync(newUser);
    }

    public async Task<Result<User>> Login(string email, string password)
    {
        var users = await this._userService.GetAllUsersAsync();
        var user = users.SingleOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
        if (user is null)
        {
            return UserErrors.NotFound;
        }
        return user;
    }
}