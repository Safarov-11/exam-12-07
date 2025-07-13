using Domain.ApiResponse;
using Domain.DTOs.CarDTOs;
using Domain.Filters;
using Infrastructure.Cars.CarInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize (Roles ="Admin, Manager")]
public class CarController(ICarService carService) : ControllerBase
{
    [HttpGet]
    public async Task<PagedResponse<List<GetCarDTO>>> GetAllAsync([FromQuery]CarFilter filter)
    {
        return await carService.GetCarsAsync(filter);
    }

    [HttpGet("{id:int}")]
    public async Task<Response<GetCarDTO?>> GetAsync(int id)
    {
        return await carService.GetCarAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateCarDTO dto)
    {
        return await carService.AddCarAsync(dto);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(UpdateCarDTO dto)
    {
        return await carService.UpdateCarAsync(dto);
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await carService.DeleteCarAsync(id);
    }
}
