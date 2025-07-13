using Domain.DTOs.CarDTOs;
using Domain.Entities;

namespace Infrastructure.Cars.CarMappers;

public static class CarMapper
{
    public static Car ToEntity(CreateCarDTO carDTO)
    {
        return new Car
        {
            Model = carDTO.Model,
            Manufacturer = carDTO.Manufacturer,
            Year = carDTO.Year,
            PricePerDay = carDTO.PricePerDay
        };
    }

    public static void ToEntity(this Car car, UpdateCarDTO carDTO)
    {
        car.Model = carDTO.Model;
        car.Manufacturer = carDTO.Manufacturer;
        car.Year = carDTO.Year;
        car.PricePerDay = carDTO.PricePerDay;
    }

    public static List<GetCarDTO> ToDTO(List<Car> cars)
    {
        return cars.Select(c => new GetCarDTO
        {
            Id = c.Id,
            Model = c.Model,
            Manufacturer = c.Manufacturer,
            Year = c.Year,
            PricePerDay = c.PricePerDay
        }).ToList();
    }

    public static GetCarDTO ToDTO(this Car car)
    {
        return new GetCarDTO
        {
            Id = car.Id,
            Model = car.Model,
            Manufacturer = car.Manufacturer,
            Year = car.Year,
            PricePerDay = car.PricePerDay
        };
    }
}
