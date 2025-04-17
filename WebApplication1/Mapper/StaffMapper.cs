using WebApplication1.Dto;
using WebApplication1.Model;
using WebApplication1.Entity;

namespace WebApplication1.Mapper;

public class StaffMapper
{
    public static StaffDto ToDto(StaffEntity staff)
    {
        return new StaffDto()
        {
            Id = staff.Id,
            FullName = staff.FullName,
            Position = staff.Position,
            Phone = staff.Phone,
            Email = staff.Email,
            Address = staff.Address,
            Age = staff.Age,
            Salary = staff.Salary,
        };
    }

    public static StaffEntity ToEntity(StaffModel staffModel)
    {
        return new StaffEntity()
        {
            FullName = staffModel.FullName,
            Position = staffModel.Position,
            Phone = staffModel.Phone,
            Address = staffModel.Address,
            Age = staffModel.Age,
            Salary = staffModel.Salary,
            Email = staffModel.Email    
        };
    }
    
    
}