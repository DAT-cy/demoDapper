using WebApplication1.Dto;
using WebApplication1.Entity;
using WebApplication1.Model;

namespace WebApplication1.Mapper;

public class DepartmentMapper
{
    public static DepartmentDto ToDto(DepartmentEntity departmentEntity)
    {
        return new DepartmentDto()
        {
            Id = departmentEntity.Id,
            Description = departmentEntity.Description,
            NameDepartment = departmentEntity.NameDepartment,
            Status = departmentEntity.Status,
            Code = departmentEntity.Code,
        };
    }

    public static DepartmentEntity ToEntity(DepartmentModel departmentModel)
    {
        return new DepartmentEntity()
        {
            Description = departmentModel.Description,
            NameDepartment = departmentModel.NameDepartment,
            Status = departmentModel.Status,
            Code = departmentModel.Code,
        };
    }
}