using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.CustomerDTOs;
using Infrastructure.Customers.CustomerInterfaces;
using Infrastructure.Customers.CustomerMappers;

namespace Infrastructure.Customers.CustomerServices;

public class CustomerService(ICustomerRepository repository) : ICustomerService
{
    public async Task<Response<string>> AddCustomerAsync(CreateCustomerDTO dto)
    {
        var customer = CustomerMapper.ToEntity(dto);
        var result = await repository.AddAsync(customer);

        return result == 0
            ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Customer created successfully");
    }

    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        var customer = await repository.GetByIdAsync(id);
        if (customer == null)
            return Response<string>.Error("Customer not found", HttpStatusCode.NotFound);

        var result = await repository.DeleteAsync(customer);

        return result == 0
            ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Customer deleted successfully");
    }

    public async Task<Response<GetCustomerDTO?>> GetCustomerAsync(int id)
    {
        var customer = await repository.GetByIdAsync(id);
        if (customer == null)
            return Response<GetCustomerDTO?>.Error("Customer not found", HttpStatusCode.NotFound);

        return Response<GetCustomerDTO?>.Success(customer.ToDTO());
    }

    public async Task<Response<List<GetCustomerDTO>>> GetCustomersAsync()
    {
        var result = await repository.GetAllAsync();
        var mapped = CustomerMapper.ToDTO(result);

        return Response<List<GetCustomerDTO>>.Success(mapped);
    }

    public async Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDTO dto)
    {
        var customer = await repository.GetByIdAsync(dto.Id);
        if (customer == null)
            return Response<string>.Error("Customer not found", HttpStatusCode.NotFound);

        CustomerMapper.ToEntity(customer, dto);
        var result = await repository.UpdateAsync(customer);

        return result == 0
            ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Customer updated successfully");
    }
}
