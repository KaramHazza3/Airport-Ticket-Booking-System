using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Services;
using FTSAirportTicketBookingSystem.Common.Validators;

namespace FTSAirportTicketBookingSystem.Services.ImportFileService;

public class ImportCsvFileService<TEntity, TDto> : IFileImportService<TEntity, TDto>
    where TEntity : class where TDto : class
{
    public async Task<Result<List<TEntity>>> ImportFileAsync(
        string filePath,
        Func<TDto, TEntity> mapper,
        IBaseService<TEntity> baseService,
        Func<TDto, int, List<ValidationError>> validator)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Result<List<TEntity>>.Failure(new Error("Csv.FilePath", "File path cannot be empty."));
        
        var entities = new List<TEntity>();
        var validationErrors = new List<ValidationError>();

        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<TDto>().ToList();

            for (int i = 0; i < records.Count; i++)
            {
                var dto = records[i];
                int rowNumber = i + 2;

                var errors = validator(dto, rowNumber);
                if (errors.Count == 0)
                {
                    entities.Add(mapper(dto));
                }
                else
                {
                    validationErrors.AddRange(errors);
                }
            }

            return await HandleAddition(baseService, validationErrors, entities);
        }
        catch (Exception ex)
        {
            var error = new Error("Csv.ReadError", $"Failed to read or parse the CSV file: {ex.Message}");
            return error;
        }
    }
    
    public async Task<Result<List<TEntity>>> HandleAddition(IBaseService<TEntity> service, List<ValidationError> validationErrors, List<TEntity> entities)
    {
        if (validationErrors.Any())
        {
            var combinedMessage = string.Join("; ", validationErrors.Select(e => $"Row {e.RowNumber} {e.PropertyName}: {e.ErrorMessage}"));
            var error = new Error("Csv.ValidationErrors", combinedMessage);
            return error;
        }

        foreach (var entity in entities)
        {
            var result = await service.AddAsync(entity);
            if (result.IsFailure)
            {
                return Result<List<TEntity>>.Failure(result.Error);
            }
        }

        return entities;
    }
}