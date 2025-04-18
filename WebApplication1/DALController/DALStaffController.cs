using System.Security.Claims;
using ClassLibrary1.DAL.common;
using ClassLibrary1.DAL.DALService;
using ClassLibrary1.DAL.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controller;
[ApiController]
[Route("api/[controller]")]

public class DALStaffController : ControllerBase
{
    
    private readonly IStaffService _staffRepo;

    public DALStaffController(IStaffService staffRepo)
    {
        _staffRepo = staffRepo;
    }
    
    [Authorize]
    [HttpGet("check-roles")]
    public IActionResult CheckRoles()
    {
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        return Ok(new { roles });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("sql")]
    public async Task<ActionResult<IEnumerable<StaffEntity>>> GetAllFromSql()
    {
        try
        {
            var staffList = await _staffRepo.GetAllStaffAsync();
            return Ok(staffList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<IEnumerable<Boolean>>> DeleteStaff(long id)
    {
        try
        {
            var staffList = await _staffRepo.DeleteStaff(id);
            return Ok(staffList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Boolean>> CreateStaff([FromBody] StaffInsertDto staff)
    {
        var staffDto = await _staffRepo.InsertStaffAsync(staff);
        return Ok(staffDto); 
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Boolean>> UpdateStaff(long id, [FromBody] StaffInsertDto staffInsert)
    {
        var staffDto = await _staffRepo.UpdateStaffAsync(id, staffInsert);
        return Ok(staffDto); 
    }
    
    [HttpGet("{id}")]
    public ActionResult<Staff> GetStaffById(long id)
    {
        var staff = _staffRepo.TryGetStaffById(id);
        return Ok(staff);
    }
    
    [HttpPut]
    public DefaultRes<int> UpdateStaff([FromBody] StaffDepartmentDto staffDepartment)
    {
        int staffDto = _staffRepo.ChosenStaff(staffDepartment);

        return DefaultRes<int>.Res( YourNamespace.Common.StatusCodeRespon.OK,ResponseMessage.SUCCESS, staffDto);
    }

    
    
}