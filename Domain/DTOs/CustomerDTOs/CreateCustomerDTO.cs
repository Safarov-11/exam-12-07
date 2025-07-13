namespace Domain.DTOs.CustomerDTOs;

public class CreateCustomerDTO
{
    public string FullName { get; set; } = null!;
    public string Phone { get; set; }
    public string Email { get; set; }
}
