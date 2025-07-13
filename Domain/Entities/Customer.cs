using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    public string Phone { get; set; }
    public string Email { get; set; }

    public List<Rental> Rentals { get; set; }
}
