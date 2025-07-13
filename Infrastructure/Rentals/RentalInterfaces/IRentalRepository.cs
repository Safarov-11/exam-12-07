using Domain.ApiResponse;
using Domain.DTOs.RentalDTOs;
using Domain.Entities;
using Domain.Filters;

namespace Infrastructure.Rentals.RentalInterfaces;

public interface IRentalRepository
{
    Task<Rental?> GetRentalByIdAsync(int id);
    Task<Car?> GetCarByIdAsync(int carId);
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    Task<Branche?> GetBrancheByIdAsync(int brancheId);
    Task<int> AddAsync(Rental rental);
    Task<int> UpdateAsync(Rental rental);
    Task<int> DeleteAsync(Rental rental);
    Task<PagedResponse<List<Rental>>> GetAllAsync(RentalFilter filter);
    Task<bool> IsCarReservedAsync(int carId, DateTime startDate, DateTime endDate);
}
