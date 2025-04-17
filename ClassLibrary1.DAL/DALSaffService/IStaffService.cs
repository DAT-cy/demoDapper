using ClassLibrary1.DAL.DTOS;

namespace ClassLibrary1.DAL.DALService;

public interface IStaffService
{
    Task<IEnumerable<Staff>> GetAllStaffAsync();

    Task<Boolean> DeleteStaff(long staffId);

    Task<bool> InsertStaffAsync(StaffInsertDto staff);

    Task<bool> UpdateStaffAsync(long staffId, StaffInsertDto staff);

    Staff TryGetStaffById(long staffId);

    int ChosenStaff(StaffDepartmentDto staffDepartment);
}