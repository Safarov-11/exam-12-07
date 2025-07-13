using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.CarDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Cars.CarInterfaces;
using Infrastructure.Cars.CarMappers;

namespace Infrastructure.Cars.CarServices;

public class CarService(ICarRepository carRepository) : ICarService
{
    public async Task<Response<string>> AddCarAsync(CreateCarDTO carDTO)
    {
        
        if (carDTO.Year > DateTime.Now.Year)
        {
            return Response<string>.Error("Incorrect information", HttpStatusCode.BadRequest);
        }
        var car = CarMapper.ToEntity(carDTO);
        var result = await carRepository.AddAsync(car);
        return result <= 0
        ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
        : Response<string>.Success(messenge: "Succes");
    }

    public async Task<Response<string>> DeleteCarAsync(int id)
    {
        var car = await carRepository.GetByIdAsync(id);
        if (car == null)
        {
            return Response<string>.Error("Car not found", HttpStatusCode.NotFound);
        }

        var result = await carRepository.DeleteAsync(car);
        return result <= 0
        ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
        : Response<string>.Success(messenge: "Succes");
    }

    public async Task<Response<GetCarDTO?>> GetCarAsync(int id)
    {
        var car = await carRepository.GetByIdAsync(id);
        if (car == null)
        {
            return Response<GetCarDTO?>.Error("Car not found", HttpStatusCode.NotFound);
        }

        var mappedCar = car.ToDTO();
        return Response<GetCarDTO?>.Success(mappedCar);
    }

    public async Task<PagedResponse<List<GetCarDTO>>> GetCarsAsync(CarFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageSize, filter.PageNumber);
        var cars = await carRepository.GetAllAsync(filter);

        var mappedCars = CarMapper.ToDTO(cars.Data!);
        return new PagedResponse<List<GetCarDTO>>(mappedCars, validFilter.PageSize, validFilter.PageNumber, cars.TotalRecords);
    }

    public async Task<Response<string>> UpdateCarAsync(UpdateCarDTO carDTO)
    {
        var car = await carRepository.GetByIdAsync(carDTO.Id);
        if (car == null)
        {
            return Response<string>.Error("Car not found", HttpStatusCode.NotFound);
        }

        if (carDTO.Year > DateTime.Now.Year)
        {
            return Response<string>.Error("Incorrect information", HttpStatusCode.BadRequest);
        }

        CarMapper.ToEntity(car, carDTO);

        var result = await carRepository.UpdateAsync(car);

        return result <= 0
        ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
        : Response<string>.Success(null, "Succes");
    }

}
