using FTSAirportTicketBookingSystem.Common.Services;
namespace FTSAirportTicketBookingSystem.Common;

public static class FilterHelper
{
    public static async Task<Result<List<T>>> FilterAsync<T>(
        IBaseService<T> baseService,
        params Func<T, bool>[] predicates)
    {
        var result = await baseService.GetAllAsync();
        if (result.IsFailure)
            return result.Error;

        var resultList = result.Value;
        Func<T, bool> combinedMatches = f => predicates.All(predicate => predicate(f));
        return resultList.Where(combinedMatches).ToList();
    }
}