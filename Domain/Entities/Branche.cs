using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Branche
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;


    public List<Car> Cars { get; set; }
}
