using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MrWorldwide.WebApi.Data.Entities;
using MrWorldwide.WebApi.Infrastructure.EntityFramework;

namespace MrWorldwide.WebApi.Data;

public class MrWorldwideDbContext : IdentityDbContext<AppUser>
{
    public MrWorldwideDbContext(DbContextOptions<MrWorldwideDbContext> options): base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplySoftDeleteQueryFilters();
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        this.ProcessCustomerInterfaces();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}