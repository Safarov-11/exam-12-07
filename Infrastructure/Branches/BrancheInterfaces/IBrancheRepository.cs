using Domain.Entities;

namespace Infrastructure.Branches.BrancheInterfaces;

public interface IBrancheRepository
{
    Task<List<Branche>> GetAllAsync();
    Task<Branche?> GetByIdAsync(int id);
    Task<int> AddAsync(Branche branche);
    Task<int> UpdateAsync(Branche branche);
    Task<int> DeleteAsync(Branche branche);
}
