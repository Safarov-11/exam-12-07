using Domain.ApiResponse;
using Domain.DTOs.RentalDTOs;
using Domain.Filters;
using Infrastructure.Rentals.RentalInterfaces;
using Infrastructure.Rentals.RentalServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController(IRentalService rentalService) : ControllerBase
{
    [HttpGet]
    public async Task<PagedResponse<List<GetRentalsDTO>>> GetAllAsync([FromQuery]RentalFilter filter)
    {
        return await rentalService.GetRentalsAsync(filter);
    }

    [HttpGet("{id:int}")]
    public async Task<Response<GetRentalsDTO?>> GetAsync(int id)
    {
        return await rentalService.GetRentalAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateRentalDTO dto)
    {
        return await rentalService.AddRentalAsync(dto);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(UpdateRentalsDTO dto)
    {
        return await rentalService.UpdateRentalAsync(dto);
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await rentalService.DeleteRentalAsync(id);
    }
}
