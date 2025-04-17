using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Entity;

namespace WebApplication1.Repository;

public class StaffRepository
{
    private readonly AppDbContext context;

    public StaffRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<StaffEntity>> GetAllStaffAsync()
    {
        return context.Staffs.Where(s => s.IsDeleted == false).ToList();
    }

    public StaffEntity GetStaffById(long id)
    {
        return context.Staffs.FirstOrDefault(s => s.Id == id);
    }

    public async Task InsertStaff(StaffEntity staff)
    {
        await context.Staffs.AddAsync(staff); // AddAsync tốt hơn
        await context.SaveChangesAsync();     // Cần lưu lại
    }

    public void updateStaff(StaffEntity staff)
    {
        context.Staffs.Update(staff);
    }

    public void ChosenStaff(long staffId, long departmentId)
    {
        var staff = context.Staffs.FirstOrDefault(s => s.Id == staffId);
        if (staff != null)
        {
            staff.DepartmentId = departmentId;
            context.Staffs.Update(staff);
            context.SaveChanges();
        }
        else
        {
            throw new Exception($"Staff with ID {staffId} not found.");
        }
    }

    public List<StaffEntity> GetStaffByDepartmentId(long departmentId)
    {
        List<StaffEntity> staffEntities = context.Staffs.Where(s => s.DepartmentId == departmentId && s.IsDeleted == false ).ToList();
        return staffEntities;
    }


}