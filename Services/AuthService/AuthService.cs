using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Errors;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Services.UserService;

namespace FTSAirportTicketBookingSystem.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUserService<Guid> _userService;

    public AuthService(IUserService<Guid> userService)
    {
        this._userService = userService;
    }
    
    public async Task<Result> RegisterAsync(string name, string email, string password, UserRole role)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return Result.Failure(AuthErrors.NotValid);
        }
        var newUser = new User(name, email, password, role);
        return await this._userService.AddUserAsync(newUser);
    }

    public async Task<Result<User>> LoginAsync(string email, string password)
    {
        var result = await this._userService.GetAllUsersAsync();
        if (result.IsFailure)
        {
            return result.Error;
        }
        var users = result.Value;
        var user = users.SingleOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
        return user == null ? AuthErrors.Unauthorized : user;
    }
}