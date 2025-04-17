using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Service.DepartmentService;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllDepartments();
    Task<DepartmentDto> CreateDepartment(DepartmentModel department);
    
   Boolean UpdateDepartment(long departmentId ,DepartmentModel department);
    
    Boolean DeleteDepartment(long departmentId );

    Task<DepartmentDto> GetDetailDepartment(long departmentId);
}