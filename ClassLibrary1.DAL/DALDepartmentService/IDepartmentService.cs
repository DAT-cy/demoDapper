using ClassLibrary1.DAL.DTOS.DepartmentDto;
using ClassLibrary1.DAL.DTOS.DepartmentModel;

namespace ClassLibrary1.DAL.DALDepartmentService;

public interface IDepartmentService
{
    List<DepartmentDALDto> GetAllDepartments();
    int CreateDepartment(DepartmentDALModel department);
    
    int UpdateDepartment(long departmentId ,DepartmentDALModel department);
    
    long DeleteDepartment(long departmentId );

    DepartmentDALDto GetDetailDepartment(long departmentId);
}