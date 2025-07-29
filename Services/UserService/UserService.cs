using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Errors;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Repository;

namespace FTSAirportTicketBookingSystem.Services.UserService;

public class UserService : IUserService
{
    private readonly IRepository _repository;
    private List<User>? _cachedUsers;
    public UserService(IRepository repository)
    {
        this._repository = repository;
    }
    public async Task<Result<User>> GetUserByIdAsync(Guid userId)
    {
        var users = await GetUsersAsync();
        var user = users.SingleOrDefault(u => u.Id == userId);
        return user is null ? UserErrors.NotFound : user;
    }
    public async Task<List<User>> GetAllUsersAsync() => await GetUsersAsync();
    public async Task<Result<User>> AddUserAsync(User user)
    {
        var users = await GetUsersAsync();
        var isExist = users.Any(u => u.Email.Equals(user.Email));
        if (isExist)
        {
            return UserErrors.AlreadyExists;
        }
        await this._repository.WriteAsync(user);
        users.Add(user);
        return user;
    }
    
    private async Task<List<User>> GetUsersAsync()
    {
        if (_cachedUsers is not null) return _cachedUsers;
        var users = await _repository.ReadAsync<User>();
        _cachedUsers = users.ToList();
        return _cachedUsers;
    }
}