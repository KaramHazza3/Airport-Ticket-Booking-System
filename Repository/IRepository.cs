namespace FTSAirportTicketBookingSystem.Repository;

public interface IRepository
{
    Task<ICollection<T>> ReadAsync<T>() where T : class;
    Task WriteAsync<T>(T data) where T : class;
}