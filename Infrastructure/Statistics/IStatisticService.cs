using Domain.ApiResponse;
using Domain.DTOs.StatisticsDTOs;

namespace Infrastructure.Statistics;

public interface IStatisticService
{
    Task<Response<decimal>> GetTotalRevenueAsync(PeriodDaysDTO daysDTO);
    Task<Response<List<string>>> GetTopModelsAsync(int year, int month);
    Task<Response<List<ActiveCustomersDTO>>> GetTopCustomersAsync();
}
