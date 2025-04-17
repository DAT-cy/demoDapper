using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Service.StaffService
{
    public interface IStaffService
    {
        Task<List<StaffDto>> GetAllStaff();
        Task<StaffDto> GetStaffById(long id);      
        Task<StaffDto> AddStaff(StaffModel staff);
        Task<bool> UpdateStaff(long staffId, StaffModel staff);
        Task<bool> DeleteStaff(long id);
        Task<Boolean> ChoseStaffForDepartment(List<StaffDepartmentModel> staffDepartmentModel);

        Task<List<StaffDto>> GetStaffByDepartmentId(long departmentId);
    }
}