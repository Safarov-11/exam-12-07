using Domain.ApiResponse;
using Domain.DTOs.BrancheDTOs;

namespace Infrastructure.Branches.BrancheInterfaces;

public interface IBrancheService
{
    Task<Response<string>> AddBranchAsync(CreateBranceDTO dto);
    Task<Response<string>> DeleteBranchAsync(int id);
    Task<Response<GetBrancheDTO?>> GetBranchAsync(int id);
    Task<Response<List<GetBrancheDTO>>> GetBranchesAsync();
    Task<Response<string>> UpdateBranchAsync(UpdateBrancheDTO dto);
}
