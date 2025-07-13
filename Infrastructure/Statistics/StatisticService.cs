using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.StatisticsDTOs;

namespace Infrastructure.Statistics;

public class StatisticService(IStatisticRepository repository) : IStatisticService
{
    public async Task<Response<decimal>> GetTotalRevenueAsync(PeriodDaysDTO daysDTO)
    {
        if (daysDTO.FromDate > daysDTO.ToDate)
            return Response<decimal>.Error("Invalid date range", HttpStatusCode.BadRequest);

        var total = await repository.GetTotalRevenueAsync(daysDTO);
        return Response<decimal>.Success(total);
    }

    public async Task<Response<List<string>>> GetTopModelsAsync(int year, int month)
    {
        if (month < 1 || month > 12)
            return Response<List<string>>.Error("Invalid month", HttpStatusCode.BadRequest);

        var result = await repository.GetTopModelsAsync(year, month);
        return Response<List<string>>.Success(result);
    }

    public async Task<Response<List<ActiveCustomersDTO>>> GetTopCustomersAsync()
    {
        var result = await repository.GetTopCustomersAsync();
        return Response<List<ActiveCustomersDTO>>.Success(result);
    }
}
