using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Services;
using FTSAirportTicketBookingSystem.Common.Validators;
using FTSAirportTicketBookingSystem.Models;

namespace FTSAirportTicketBookingSystem.Services.ImportFileService;

public interface IFileImportService<TEntity, TDto> 
{
    Task<Result<List<TEntity>>> ImportFileAsync(
        string filePath,
        Func<TDto, TEntity> mapper,
        IBaseService<TEntity> baseService,
        Func<TDto, int, List<ValidationError>> validator);
}