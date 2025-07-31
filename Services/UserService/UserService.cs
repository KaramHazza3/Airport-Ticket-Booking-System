using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Errors;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Repository;

namespace FTSAirportTicketBookingSystem.Services.UserService;

public class UserService : IUserService
{
    private readonly IRepository _repository;
    public UserService(IRepository repository)
    {
        this._repository = repository;
    }
    
    public async Task<Result<User>> GetAsync(Func<User, bool> predicate)
    {
        var users = await this._repository.ReadAsync<User>();
        var usersList = users.ToList();
        var user = usersList.SingleOrDefault(predicate);
        return user == null ? UserErrors.NotFound : user;
    }

    public async Task<Result<ICollection<User>>> GetAllAsync()
    {
        var users = await this._repository.ReadAsync<User>();
        return users.ToList();
    }

    public async Task<Result<User>> AddAsync(User user)
    {
        var users = await this._repository.ReadAsync<User>();
        var usersList = users.ToList();
        var isExist = usersList.Any(u => u.Email.Equals(user.Email));
        if (isExist)
        {
            return UserErrors.AlreadyExists;
        }
        return await this._repository.WriteAsync(user);
    }

    public async Task<Result> DeleteAsync(Guid userId)
    {
        var users = await this._repository.ReadAsync<User>();
        var usersList = users.ToList();
        var user = usersList.SingleOrDefault(b => b.Id == userId);
        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound);
        }
        await this._repository.DeleteAsync(user);
        return Result.Success();
    }

    public async Task<Result<User>> UpdateAsync(Guid id, User user)
    {
        var users = await this._repository.ReadAsync<User>();
        var usersList = users.ToList();
        var isExist = usersList.Exists(b => b.Id == id);
        return isExist ?  await this._repository.UpdateAsync(user): UserErrors.NotFound;
    }
}