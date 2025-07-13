using Domain.ApiResponse;
using Domain.Entities;

namespace Infrastructure.Customers.CustomerInterfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<int> AddAsync(Customer customer);
    Task<int> UpdateAsync(Customer customer);
    Task<int> DeleteAsync(Customer customer);
}
