using Domain.ApiResponse;
using Domain.DTOs.BrancheDTOs;
using Infrastructure.Branches.BrancheInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize (Roles ="Admin, Manager")]
public class BrancheController(IBrancheService brancheService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetBrancheDTO>>> GetAllAsync()
    {
        return await brancheService.GetBranchesAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Response<GetBrancheDTO?>> GetAsync(int id)
    {
        return await brancheService.GetBranchAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateBranceDTO dto)
    {
        return await brancheService.AddBranchAsync(dto);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(UpdateBrancheDTO dto)
    {
        return await brancheService.UpdateBranchAsync(dto);
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await brancheService.DeleteBranchAsync(id);
    }
}
