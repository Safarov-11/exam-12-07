using Domain.DTOs.BrancheDTOs;
using Domain.Entities;

namespace Infrastructure.Branches.BrancheMappers;

public static class BrancheMapper
{
    public static Branche ToEntity(CreateBranceDTO dto)
    {
        return new Branche
        {
            Name = dto.Name,
            Location = dto.Location
        };
    }

    public static void ToEntity(this Branche branche, UpdateBrancheDTO dto)
    {
        branche.Name = dto.Name;
        branche.Location = dto.Location;
    }

    public static List<GetBrancheDTO> ToDTO(List<Branche> branchees)
    {
        return branchees.Select(b => new GetBrancheDTO
        {
            Id = b.Id,
            Name = b.Name,
            Location = b.Location
        }).ToList();
    }

    public static GetBrancheDTO ToDTO(this Branche branche)
    {
        return new GetBrancheDTO
        {
            Id = branche.Id,
            Name = branche.Name,
            Location = branche.Location
        };
    }
}
