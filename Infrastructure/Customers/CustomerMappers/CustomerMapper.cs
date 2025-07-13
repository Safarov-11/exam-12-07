using Domain.DTOs.CustomerDTOs;
using Domain.Entities;

namespace Infrastructure.Customers.CustomerMappers;

public static class CustomerMapper
{
    public static Customer ToEntity(CreateCustomerDTO dto)
    {
        return new Customer
        {
            FullName = dto.FullName,
            Phone = dto.Phone,
            Email = dto.Email
        };
    }

    public static void ToEntity(this Customer customer, UpdateCustomerDTO dto)
    {
        customer.FullName = dto.FullName;
        customer.Phone = dto.Phone;
        customer.Email = dto.Email;
    }

    public static List<GetCustomerDTO> ToDTO(List<Customer> customers)
    {
        return customers.Select(c => new GetCustomerDTO
        {
            Id = c.Id,
            FullName = c.FullName,
            Phone = c.Phone,
            Email = c.Email
        }).ToList();
    }

    public static GetCustomerDTO ToDTO(this Customer customer)
    {
        return new GetCustomerDTO
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Phone = customer.Phone,
            Email = customer.Email
        };
    }
}
