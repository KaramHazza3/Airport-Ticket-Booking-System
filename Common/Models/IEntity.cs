namespace FTSAirportTicketBookingSystem.Common.Models;

public interface IEntity<T>
{
    T Id { get; init; }
}