using Domain.ApiResponse;
using Domain.DTOs.StatisticsDTOs;
using Infrastructure.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize (Roles ="Admin, Manager")]
public class StatisticsController(IStatisticService statistic) : ControllerBase
{
    [HttpPost("total-rentals-prices")]
    public async Task<Response<decimal>> GetTotalRevenue([FromBody] PeriodDaysDTO daysDTO)
    {
        return await statistic.GetTotalRevenueAsync(daysDTO);
    }

    [HttpGet("top-models")]
    public async Task<Response<List<string>>> GetTopModels([FromQuery] int year, [FromQuery] int month)
    {
        return await statistic.GetTopModelsAsync(year, month);
    }

    [HttpGet("top-customers")]
    public async Task<Response<List<ActiveCustomersDTO>>> GetTopCustomers()
    {
        var customers = await statistic.GetTopCustomersAsync();

        return Response<List<ActiveCustomersDTO>>.Success(customers.Data);
    }
}
