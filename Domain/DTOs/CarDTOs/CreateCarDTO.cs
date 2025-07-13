namespace Domain.DTOs.CarDTOs;

public class CreateCarDTO
{
    public string Model { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; } 
}
