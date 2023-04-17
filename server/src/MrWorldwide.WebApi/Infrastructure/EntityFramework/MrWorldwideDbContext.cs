using Microsoft.EntityFrameworkCore;

namespace MrWorldwide.WebApi.Infrastructure.EntityFramework;

public class MrWorldwideDbContext : DbContext
{
    public MrWorldwideDbContext(DbContextOptions<MrWorldwideDbContext> options): base(options)
    {
        
    }
}