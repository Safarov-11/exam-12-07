using Domain.ApiResponse;
using Domain.Entities;
using Domain.Filters;

namespace Infrastructure.Cars.CarInterfaces;

public interface ICarRepository
{
    Task<int> AddAsync(Car car);
    Task<int> UpdateAsync(Car car);
    Task<int> DeleteAsync(Car car);
    Task<Car?> GetByIdAsync(int id);
    Task<PagedResponse<List<Car>>> GetAllAsync(CarFilter filter);
}
