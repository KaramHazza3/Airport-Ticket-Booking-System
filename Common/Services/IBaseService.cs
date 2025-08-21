
using FTSAirportTicketBookingSystem.Common.Models;

namespace FTSAirportTicketBookingSystem.Common.Services;

public interface IBaseService<TEntity, TId>
{
    Task<Result<TEntity>> AddAsync(TEntity data);
}