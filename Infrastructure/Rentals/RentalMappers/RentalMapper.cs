using Domain.DTOs.RentalDTOs;
using Domain.Entities;

namespace Infrastructure.Rentals.RentalMappers;

public static class RentalMapper
{
    public static Rental ToEntity(CreateRentalDTO dto, decimal pricePerDay)
    {
        var totalDays = (dto.EndDate - dto.StartDate).Days;
        return new Rental
        {
            CarId = dto.CarId,
            CustomerId = dto.CustomerId,
            BranchId = dto.BranchId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalCost = totalDays * pricePerDay
        };
    }

    public static void ToEntity(this Rental rental, UpdateRentalsDTO dto)
    {
        rental.CarId = dto.CarId;
        rental.CustomerId = dto.CustomerId;
        rental.BranchId = dto.BranchId;
        rental.StartDate = dto.StartDate;
        rental.EndDate = dto.EndDate;
    }

    public static GetRentalsDTO ToDTO(this Rental rental)
    {
        return new GetRentalsDTO
        {
            Id = rental.Id,
            CarId = rental.CarId,
            CustomerId = rental.CustomerId,
            BranchId = rental.BranchId,
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            TotalCost = rental.TotalCost
        };
    }

    public static List<GetRentalsDTO> ToDTO(List<Rental> rentals)
    {
        return rentals.Select(r => new GetRentalsDTO
        {
            Id = r.Id,
            CarId = r.CarId,
            CustomerId = r.CustomerId,
            BranchId = r.BranchId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            TotalCost = r.TotalCost
        }).ToList();
    }
}
