using Domain.Entities;
using Infrastructure.Customers.CustomerInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Customers.CustomerRepositories;

public class CustomerRepository(DataContext context) : ICustomerRepository
{
    public async Task<List<Customer>> GetAllAsync()
    {
        return await context.Customers.ToListAsync();
    }

    public async Task<int> AddAsync(Customer customer)
    {
        await context.Customers.AddAsync(customer);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Customer customer)
    {
        context.Customers.Update(customer);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Customer customer)
    {
        context.Customers.Remove(customer);
        return await context.SaveChangesAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await context.Customers.FindAsync(id);
    }

}
