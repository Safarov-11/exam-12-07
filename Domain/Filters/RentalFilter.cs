using Domain.Paginations;

namespace Domain.Filters;

public class RentalFilter : ValidFilter
{
    public int? CustomerId { get; set; }
    public int? CarId { get; set; }
}
