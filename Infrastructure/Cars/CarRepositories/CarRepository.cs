using Domain.ApiResponse;
using Domain.Entities;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Cars.CarInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Cars.CarRepositories;

public class CarRepository(DataContext context) : ICarRepository
{
    public async Task<int> AddAsync(Car car)
    {
        await context.Cars.AddAsync(car);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Car car)
    {
        context.Cars.Update(car);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Car car)
    {
        context.Cars.Remove(car);
        return await context.SaveChangesAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        return await context.Cars.FindAsync(id);
    }

    public async Task<PagedResponse<List<Car>>> GetAllAsync(CarFilter filter)
    {
        var query = context.Cars.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Model))
        {
            query = query.Where(c => c.Model.ToLower().Trim().Contains(filter.Model.ToLower().Trim()));
        }

        if (!string.IsNullOrEmpty(filter.Manufacturer))
        {
            query = query.Where(c => c.Manufacturer.ToLower().Trim().Contains(filter.Manufacturer.ToLower().Trim()));
        }

        if (filter.Year != null)
        {
            query = query.Where(c => c.Year == filter.Year);
        }

        var pagination = new Pagination<Car>(query);
        return await pagination.GetPagedResponseAsync(filter.PageSize, filter.PageNumber);
    }

}