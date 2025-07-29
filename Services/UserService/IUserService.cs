using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Models;

namespace FTSAirportTicketBookingSystem.Services.UserService;

public interface IUserService
{
    Task<Result<User>> GetUserByIdAsync(Guid userId);
    Task<List<User>> GetAllUsersAsync();
    Task<Result<User>> AddUserAsync(User user);
}