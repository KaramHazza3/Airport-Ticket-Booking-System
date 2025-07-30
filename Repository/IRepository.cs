namespace FTSAirportTicketBookingSystem.Repository;

public interface IRepository
{
    Task<ICollection<T>> ReadAsync<T>() where T : class;
    Task<T> WriteAsync<T>(T data) where T : class;
    Task DeleteAsync<T>(T data) where T : class;
    Task<T> UpdateAsync<T>(T data) where T : class;
}