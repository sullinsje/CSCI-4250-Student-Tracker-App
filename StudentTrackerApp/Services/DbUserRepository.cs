using Microsoft.EntityFrameworkCore;

namespace StudentTrackerApp.Services;
public class DbUserRepository : IUserRepository
{
    private readonly ApplicationIdentityDbContext _db;

    public DbUserRepository(ApplicationIdentityDbContext db)
    {
        _db = db;
    }

   
}