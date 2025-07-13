using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Branches.BrancheInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Branches.BranchRepositories;

public class BrancheRepository(DataContext context) : IBrancheRepository
{
    public async Task<List<Branche>> GetAllAsync()
    {
        return await context.Branches.ToListAsync();
    }

    public async Task<int> AddAsync(Branche branche)
    {
        await context.Branches.AddAsync(branche);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Branche branche)
    {
        context.Branches.Update(branche);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Branche branche)
    {
        context.Branches.Remove(branche);
        return await context.SaveChangesAsync();
    }

    public async Task<Branche?> GetByIdAsync(int id)
    {
        return await context.Branches.FindAsync(id);
    }
}
