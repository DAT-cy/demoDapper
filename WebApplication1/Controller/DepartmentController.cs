using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Model;
using WebApplication1.Service.DepartmentService;

namespace WebApplication1.Controller;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        this.departmentService = departmentService; 
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] DepartmentModel departmentModel)
    {
        var departmentDto = await departmentService.CreateDepartment(departmentModel);
        return Ok(departmentDto);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetALLDepartment()
    {
        var departmentDto = await departmentService.GetAllDepartments();
        return Ok(departmentDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateDepartment(long id, [FromBody] DepartmentModel departmentModel)
    {
        var update =  departmentService.UpdateDepartment(id, departmentModel);
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteDepartment(long id)
    {
        var delete =   departmentService.DeleteDepartment(id);
        return Ok(delete);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDto>> GetDetailDepartment(long id)
    {
        var departmentDto = await departmentService.GetDetailDepartment(id);
        return Ok(departmentDto);
    }

}
