using Domain.ApiResponse;
using Domain.DTOs.RentalDTOs;
using Domain.Filters;

namespace Infrastructure.Rentals.RentalInterfaces;

public interface IRentalService
{
    Task<Response<string>> AddRentalAsync(CreateRentalDTO dto);
    Task<Response<string>> DeleteRentalAsync(int id);
    Task<Response<GetRentalsDTO?>> GetRentalAsync(int id);
    Task<PagedResponse<List<GetRentalsDTO>>> GetRentalsAsync(RentalFilter filter);
    Task<Response<string>> UpdateRentalAsync(UpdateRentalsDTO dto);
}
