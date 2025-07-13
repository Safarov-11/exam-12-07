using Domain.ApiResponse;
using Domain.DTOs.CustomerDTOs;

namespace Infrastructure.Customers.CustomerInterfaces;

public interface ICustomerService
{
    Task<Response<string>> AddCustomerAsync(CreateCustomerDTO dto);
    Task<Response<string>> DeleteCustomerAsync(int id);
    Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDTO dto);
    Task<Response<GetCustomerDTO?>> GetCustomerAsync(int id);
    Task<Response<List<GetCustomerDTO>>> GetCustomersAsync();
}
