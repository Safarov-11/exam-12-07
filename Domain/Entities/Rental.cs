using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Rental
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalCost { get; set; }


    public Car Car { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public Branche Branche { get; set; } = null!;
}
