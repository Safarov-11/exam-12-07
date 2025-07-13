using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.BrancheDTOs;
using Infrastructure.Branches.BrancheInterfaces;
using Infrastructure.Branches.BrancheMappers;

namespace Infrastructure.Branches.BrancheServices;

public class BrancheService(IBrancheRepository repository) : IBrancheService
{
    public async Task<Response<string>> AddBranchAsync(CreateBranceDTO dto)
    {
        var branch = BrancheMapper.ToEntity(dto);
        var result = await repository.AddAsync(branch);

        return result == 0
            ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Branch created successfully");
    }

    public async Task<Response<string>> DeleteBranchAsync(int id)
    {
        var branch = await repository.GetByIdAsync(id);
        if (branch == null)
            return Response<string>.Error("Branch not found", HttpStatusCode.NotFound);

        var result = await repository.DeleteAsync(branch);

        return result == 0
            ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Branch deleted successfully");
    }

    public async Task<Response<GetBrancheDTO?>> GetBranchAsync(int id)
    {
        var branch = await repository.GetByIdAsync(id);
        if (branch == null)
            return Response<GetBrancheDTO?>.Error("Branch not found", HttpStatusCode.NotFound);

        return Response<GetBrancheDTO?>.Success(branch.ToDTO());
    }

    public async Task<Response<List<GetBrancheDTO>>> GetBranchesAsync()
    {
        var result = await repository.GetAllAsync();
        var mapped = BrancheMapper.ToDTO(result);

        return Response<List<GetBrancheDTO>>.Success(mapped);
    }

    public async Task<Response<string>> UpdateBranchAsync(UpdateBrancheDTO dto)
    {
        var branch = await repository.GetByIdAsync(dto.Id);
        if (branch == null)
            return Response<string>.Error("Branch not found", HttpStatusCode.NotFound);

        BrancheMapper.ToEntity(branch, dto);
        var result = await repository.UpdateAsync(branch);

        return result == 0
            ? Response<string>.Error("Something went wrong", HttpStatusCode.InternalServerError)
            : Response<string>.Success("Branch updated successfully");
    }
}
