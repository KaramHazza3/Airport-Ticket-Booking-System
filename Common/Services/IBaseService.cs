namespace FTSAirportTicketBookingSystem.Common.Services;

public interface IBaseService<T>
{
    Task<Result<T>> GetAsync(Func<T, bool> predicate);
    Task<Result<ICollection<T>>> GetAllAsync();
    Task<Result<T>> AddAsync(T data);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<T>> UpdateAsync(Guid id, T data);
}