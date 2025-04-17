
using WebApplication1.Dto;
using WebApplication1.Entity;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Service.DepartmentService;

public class DepartmentService : IDepartmentService
{
    private readonly DepartmentRepository departmentRepository;

    public DepartmentService(DepartmentRepository departmentRepository)
    {
        this.departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentDto>> GetAllDepartments()
    {
        List<DepartmentEntity> departmentEntities = departmentRepository.GetDepartmentsAsync().GetAwaiter().GetResult();
        
        List<DepartmentDto> departmentDtos = departmentEntities.Select(Mapper.DepartmentMapper.ToDto).ToList();
        return departmentDtos;
        
    }

    public async Task<DepartmentDto> CreateDepartment(DepartmentModel department)
    {
        DepartmentEntity departmentEntity = Mapper.DepartmentMapper.ToEntity(department);
        DepartmentEntity departmentEntity1 = await departmentRepository.InsertDepartmentAsync(departmentEntity);
        DepartmentDto departmentDto = Mapper.DepartmentMapper.ToDto(departmentEntity1);
        return departmentDto;
    }

    public Boolean UpdateDepartment(long departmentId , DepartmentModel department)
    {
        DepartmentEntity departmentEntity = departmentRepository.FindByDepartmentId(departmentId);
        if (departmentEntity == null)
        {
            throw new ErrorResponse(ErrorCode.STAFF_NOT_FOUND);
        }
        departmentEntity.Description = department.Description;
        departmentEntity.NameDepartment = department.NameDepartment;
        departmentEntity.Status = department.Status;
        departmentEntity.Code = department.Code;
        departmentRepository.UpdateDepartmentAsync(departmentEntity);
        return true;
    }

    public Boolean DeleteDepartment(long departmentId )
    {
        DepartmentEntity departmentEntity = departmentRepository.FindByDepartmentId(departmentId);
        if (departmentEntity == null)
        {
            throw new ErrorResponse(ErrorCode.DEPARTMENT_NOT_FOUND);
        }
        departmentEntity.IsDeleted = true;
        departmentRepository.UpdateDepartmentAsync(departmentEntity);
        return true;
    }
    
    public async Task<DepartmentDto> GetDetailDepartment(long departmentId )
    {
        DepartmentEntity departmentEntity = departmentRepository.FindByDepartmentId(departmentId);
        if (departmentEntity == null)
        {
            throw new ErrorResponse(ErrorCode.DEPARTMENT_NOT_FOUND);
        }
        DepartmentDto departmentDto = Mapper.DepartmentMapper.ToDto(departmentEntity);
        return departmentDto;
    }
    
    
}
