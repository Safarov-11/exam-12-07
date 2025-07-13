using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.RentalDTOs;
using Domain.Filters;
using Infrastructure.Cars.CarRepositories;
using Infrastructure.Rentals.RentalInterfaces;
using Infrastructure.Rentals.RentalMappers;

namespace Infrastructure.Rentals.RentalServices;

public class RentalService(IRentalRepository repository) : IRentalService
{
    public async Task<Response<string>> AddRentalAsync(CreateRentalDTO dto)
    {
        var car = await repository.GetCarByIdAsync(dto.CarId);
        if (car == null)
            return Response<string>.Error("Car not found", HttpStatusCode.NotFound);

        var customer = await repository.GetCustomerByIdAsync(dto.CustomerId);
        if (customer == null)
            return Response<string>.Error("customer not found", HttpStatusCode.NotFound);
      
        var branche = await repository.GetBrancheByIdAsync(dto.BranchId);
        if (branche == null)
            return Response<string>.Error("Branche not found", HttpStatusCode.NotFound);

        var isReserved = await repository.IsCarReservedAsync(dto.CarId, dto.StartDate, dto.EndDate);
        if (isReserved)
            return Response<string>.Error("Car is already reserved for selected dates", HttpStatusCode.BadRequest);

        var rental = RentalMapper.ToEntity(dto, car.PricePerDay);
        var result = await repository.AddAsync(rental);

        return result == 0
            ? Response<string>.Error("Could not create rental", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Rental created successfully");
    }

    public async Task<Response<string>> DeleteRentalAsync(int id)
    {
        var rental = await repository.GetRentalByIdAsync(id);
        if (rental == null)
            return Response<string>.Error("Rental not found", HttpStatusCode.NotFound);

        var result = await repository.DeleteAsync(rental);

        return result == 0
            ? Response<string>.Error("Could not delete rental", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Rental deleted successfully");
    }

    public async Task<Response<GetRentalsDTO?>> GetRentalAsync(int id)
    {
        var rental = await repository.GetRentalByIdAsync(id);
        if (rental == null)
            return Response<GetRentalsDTO?>.Error("Rental not found", HttpStatusCode.NotFound);

        return Response<GetRentalsDTO?>.Success(rental.ToDTO());
    }

    public async Task<PagedResponse<List<GetRentalsDTO>>> GetRentalsAsync(RentalFilter filter)
    {
        var result = await repository.GetAllAsync(filter);
        var mapped = RentalMapper.ToDTO(result.Data!);

        return new PagedResponse<List<GetRentalsDTO>>(mapped, result.PageSize, result.PageNumber, result.TotalRecords);
    }

    public async Task<Response<string>> UpdateRentalAsync(UpdateRentalsDTO dto)
    {
        var rental = await repository.GetRentalByIdAsync(dto.Id);
        if (rental == null)
            return Response<string>.Error("Rental not found", HttpStatusCode.NotFound);

        var car = await repository.GetCarByIdAsync(dto.CarId);
        if (car == null)
            return Response<string>.Error("Car not found", HttpStatusCode.NotFound);

        var customer = await repository.GetCustomerByIdAsync(dto.CustomerId);
        if (customer == null)
            return Response<string>.Error("customer not found", HttpStatusCode.NotFound);
      
        var branche = await repository.GetBrancheByIdAsync(dto.BranchId);
        if (branche == null)
            return Response<string>.Error("Branche not found", HttpStatusCode.NotFound);

        RentalMapper.ToEntity(rental, dto);
        var result = await repository.UpdateAsync(rental);

        return result == 0
            ? Response<string>.Error("Could not update rental", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Rental updated successfully");
    }
}
