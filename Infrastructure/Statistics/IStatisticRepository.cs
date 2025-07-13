using Domain.DTOs.StatisticsDTOs;

namespace Infrastructure.Statistics;

public interface IStatisticRepository
{
    Task<decimal> GetTotalRevenueAsync(PeriodDaysDTO daysDTO);
    Task<List<string>> GetTopModelsAsync(int year, int month);
    Task<List<ActiveCustomersDTO>> GetTopCustomersAsync();
}
