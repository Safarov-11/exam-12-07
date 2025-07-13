using Domain.ApiResponse;
using Domain.DTOs.CustomerDTOs;
using Infrastructure.Customers.CustomerInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetCustomerDTO>>> GetAllAsync()
    {
        return await customerService.GetCustomersAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Response<GetCustomerDTO?>> GetAsync(int id)
    {
        return await customerService.GetCustomerAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateCustomerDTO dto)
    {
        return await customerService.AddCustomerAsync(dto);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(UpdateCustomerDTO dto)
    {
        return await customerService.UpdateCustomerAsync(dto);
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await customerService.DeleteCustomerAsync(id);
    }
}
