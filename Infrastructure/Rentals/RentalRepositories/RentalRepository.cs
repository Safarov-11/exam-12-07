using Domain.ApiResponse;
using Domain.DTOs.RentalDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Data;
using Infrastructure.Rentals.RentalInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Rentals.RentalRepositories;

public class RentalRepository(DataContext context) : IRentalRepository
{
    public async Task<Rental?> GetRentalByIdAsync(int id)
    {
        return await context.Rentals.FindAsync(id);
    }

    public async Task<Car?> GetCarByIdAsync(int carId)
    {
        return await context.Cars.FindAsync(carId);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await context.Customers.FindAsync(customerId);
    }

    public async Task<Branche?> GetBrancheByIdAsync(int brancheId)
    {
        return await context.Branches.FindAsync(brancheId);
    }

    public async Task<int> AddAsync(Rental rental)
    {
        await context.Rentals.AddAsync(rental);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Rental rental)
    {
        context.Rentals.Update(rental);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Rental rental)
    {
        context.Rentals.Remove(rental);
        return await context.SaveChangesAsync();
    }

    public async Task<PagedResponse<List<Rental>>> GetAllAsync(RentalFilter filter)
    {
        var query = context.Rentals.Include(r => r.Car).Include(r => r.Customer).Include(r => r.Branche).AsQueryable();

        if (filter.CustomerId != null)
        {
            query = query.Where(r => r.CustomerId == filter.CustomerId);
        }

        if (filter.CarId != null)
        {
            query = query.Where(r => r.CarId == filter.CarId);
        }

        var pagination = new Pagination<Rental>(query);
        return await pagination.GetPagedResponseAsync(filter.PageSize, filter.PageNumber);
    }

    public async Task<bool> IsCarReservedAsync(int carId, DateTime startDate, DateTime endDate)
    {
        return await context.Rentals.AnyAsync(r => r.CarId == carId &&
        (r.StartDate <= endDate) && (r.EndDate >= startDate));
    }

}
