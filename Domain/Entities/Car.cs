using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Car
{
    public int Id { get; set; }
    [Required]
    public string Model { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; } 
    
    
    public List<Rental> Rentals { get; set; }

}
