using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Models;
using FTSAirportTicketBookingSystem.Common.Services;
using FTSAirportTicketBookingSystem.Common.Validators;
using FTSAirportTicketBookingSystem.Common.Validators.CsvValidators.Models;
using FTSAirportTicketBookingSystem.Models;

namespace FTSAirportTicketBookingSystem.Services.ImportFileService;

public interface IFileImportService
{
    Task<Result<List<TEntity>>> ImportFileAsync<TEntity, TDto, TId>(
        string filePath,
        Func<TDto, TEntity> mapper,
        IBaseService<TEntity, TId> baseService,
        Func<TDto, int, List<CsvValidationError>> validator)
        where TEntity : IEntity<TId>;
}