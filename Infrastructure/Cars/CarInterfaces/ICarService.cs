using Domain.ApiResponse;
using Domain.DTOs.CarDTOs;
using Domain.Filters;

namespace Infrastructure.Cars.CarInterfaces;

public interface ICarService
{
    Task<Response<string>> AddCarAsync(CreateCarDTO carDTO);
    Task<Response<string>> UpdateCarAsync(UpdateCarDTO carDTO);
    Task<Response<string>> DeleteCarAsync(int id);
    Task<Response<GetCarDTO?>> GetCarAsync(int id);
    Task<PagedResponse<List<GetCarDTO>>> GetCarsAsync(CarFilter filter);
}
