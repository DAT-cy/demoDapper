using ClassLibrary1.DAL.common;
using ClassLibrary1.DAL.DALDepartmentService;
using ClassLibrary1.DAL.DTOS.DepartmentDto;
using ClassLibrary1.DAL.DTOS.DepartmentModel;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Common;


[ApiController]
[Route("[controller]")]
public class DALDepartmentController : ControllerBase
{
    private readonly IDepartmentService departmentService;

    public DALDepartmentController(IDepartmentService departmentService)
    {
        this.departmentService = departmentService; 
    }

    [HttpPost]
    public  DefaultRes<int> CreateDepartment([FromBody] DepartmentDALModel departmentDalModel)
    {
        var departmentDto =  departmentService.CreateDepartment(departmentDalModel);
        return DefaultRes<int>.Res(YourNamespace.Common.StatusCodeRespon.OK,
            ResponseMessage.CREATED,
            departmentDto);
    }
    
    [HttpGet]
    public  DefaultRes<List<DepartmentDALDto>> GetALLDepartment()
    {
        var departmentDto =  departmentService.GetAllDepartments();
        return DefaultRes<List<DepartmentDALDto>>.Res(YourNamespace.Common.StatusCodeRespon.OK,
            ResponseMessage.GET_LIST,
            departmentDto);
    }

    [HttpPut("{id}")]
    public DefaultRes<int> UpdateDepartment(long id, [FromBody] DepartmentDALModel departmentDalModel)
    {
        var update =  departmentService.UpdateDepartment(id, departmentDalModel);
        return DefaultRes<int>.Res(StatusCodeRespon.OK ,
            ResponseMessage.UPDATED,
            update);
    }

    [HttpDelete("{id}")]
    public DefaultRes<long> DeleteDepartment(long id)
    {
        var delete =  departmentService.DeleteDepartment(id);
        return DefaultRes<long>.Res(StatusCodeRespon.OK ,
            ResponseMessage.DELETED,
            delete);
    }
    
    [HttpGet("{id}")]
    public DefaultRes<DepartmentDALDto> GetDetailDepartment(long id)
    {
        var departmentDto = departmentService.GetDetailDepartment(id);
        return DefaultRes<DepartmentDALDto>.Res(StatusCodeRespon.OK,
            ResponseMessage.GET_LIST,
            departmentDto);
    }
}