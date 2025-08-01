using FTSAirportTicketBookingSystem.Common;
using FTSAirportTicketBookingSystem.Common.Errors;
using FTSAirportTicketBookingSystem.Common.Services;
using FTSAirportTicketBookingSystem.Models;
using FTSAirportTicketBookingSystem.Repository;

namespace FTSAirportTicketBookingSystem.Services.FlightService;

public class FlightService : IFlightService, IFilterService<Flight>
{
    private readonly IRepository _repository;

    public FlightService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Flight>> GetAsync(Func<Flight, bool> predicate)
    {
        var flights = await this._repository.ReadAsync<Flight>();
        var flightsList = flights.ToList();
        var flight = flightsList.SingleOrDefault(predicate);
        if (flight == null)
        {
            return FlightErrors.NotFound;
        }

        return flight;
    }

    public async Task<Result<ICollection<Flight>>> GetAllAsync()
    {
        var flights = await this._repository.ReadAsync<Flight>();
        return flights.ToList();
    }

    public async Task<Result<Flight>> AddAsync(Flight data)
    {
        var flights = await this._repository.ReadAsync<Flight>();
        var flightsList = flights.ToList();
        var isExist = flightsList.Any(f => f.Id == data.Id);
        return isExist ? FlightErrors.AlreadyExists : await this._repository.WriteAsync(data);
    }

    public async Task<Result> DeleteAsync(Guid flightId)
    {
        var flights = await this._repository.ReadAsync<Booking>();
        var flightsList = flights.ToList();
        var result = flightsList.SingleOrDefault(b => b.Id == flightId);
        if (result == null)
        {
            return Result.Failure(FlightErrors.NotFound);
        }
        await this._repository.DeleteAsync(result);
        return Result.Success();
    }

    public async Task<Result<Flight>> UpdateAsync(Guid id, Flight data)
    {
        var flights = await this._repository.ReadAsync<Booking>();
        var flightsList = flights.ToList();
        var isExist = flightsList.Exists(b => b.Id == id);
        return isExist ?  await this._repository.UpdateAsync(data): BookingErrors.NotFound;
    }
    
    public async Task<Result<List<Flight>>> FilterAsync(params Func<Flight, bool>[] match)
    {
        return await FilterHelper.FilterAsync(this, match);
    }
}