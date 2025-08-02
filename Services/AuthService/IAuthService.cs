using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Models;

namespace FTSAirportTicketBookingSystem.Services.AuthService;

public interface IAuthService
{
    Task<Result> RegisterAsync(string name, string email, string password, UserRole role);
    Task<Result<User>> LoginAsync(string email, string password);
}