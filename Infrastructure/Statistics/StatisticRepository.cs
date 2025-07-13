using Domain.DTOs.StatisticsDTOs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Statistics;

public class StatisticRepository(DataContext context) : IStatisticRepository
{
    public async Task<decimal> GetTotalRevenueAsync(PeriodDaysDTO prDays)
    {
        return await context.Rentals
            .Where(r => r.StartDate == prDays.FromDate && r.EndDate == prDays.ToDate)
            .SumAsync(r => r.TotalCost);
    }

    public async Task<List<string>> GetTopModelsAsync(int year, int month)
    {
        return await context.Rentals
            .Where(r => r.StartDate.Year == year && r.StartDate.Month == month)
            .GroupBy(r => r.Car.Model)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToListAsync();
    }

    public async Task<List<ActiveCustomersDTO>> GetTopCustomersAsync()
    {
        return await context.Rentals
            .GroupBy(r => r.Customer.FullName)
            .OrderByDescending(g => g.Count())
            .Select(g => new ActiveCustomersDTO
            {
                FullName = g.Key,
                RentalCount = g.Count()
            })
            .ToListAsync();
    }
}
