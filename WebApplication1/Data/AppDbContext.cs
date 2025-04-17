using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<StaffEntity> Staffs { get; set; }
    public DbSet<DepartmentEntity> Departments { get; set; }
    
    public DbSet<UserEntity> Users { get; set; }
    
    
}
