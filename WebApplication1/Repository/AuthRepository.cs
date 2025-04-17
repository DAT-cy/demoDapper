using WebApplication1.Data;
using WebApplication1.Entity;

namespace WebApplication1.Repository;

public class AuthRepository
{
    private readonly AppDbContext context;

    public AuthRepository(AppDbContext context)
    {
        this.context = context;
    }

    public UserEntity InsertUserAsync(String userName, String password)
    {
        return context.Users.Where(u => u.UserName == userName && u.Password == password).FirstOrDefault();
    }
}