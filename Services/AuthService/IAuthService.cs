using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Models;

namespace FTSAirportTicketBookingSystem.Services.AuthService;

public interface IAuthService
{
    Task<Result> Register(string name, string email, string password, UserRole role);
    Task<Result<User>> Login(string email, string password);
}