using WebApplication1.Data;
using WebApplication1.Entity;

namespace WebApplication1.Repository;

public class DepartmentRepository
{
    private readonly AppDbContext context;

    public DepartmentRepository(AppDbContext context)
    {
        this.context = context;
    }
    
    public async Task<DepartmentEntity> InsertDepartmentAsync(DepartmentEntity departmentEntity)
    {
        context.Departments.Add(departmentEntity);
        await context.SaveChangesAsync();

        return departmentEntity; 
    }

    public async Task UpdateDepartmentAsync(DepartmentEntity departmentEntity)
    {
        context.Departments.Update(departmentEntity);
        await context.SaveChangesAsync();
    }
    

    public async Task<List<DepartmentEntity>> GetDepartmentsAsync()
    {
        return context.Departments.Where(d => d.IsDeleted == false).ToList();
    }
    
    public DepartmentEntity FindByDepartmentId(long departmentId)
    {
        return context.Departments
            .Where(d => d.Id == departmentId && d.IsDeleted == false)
            .FirstOrDefault(); 
    }



}