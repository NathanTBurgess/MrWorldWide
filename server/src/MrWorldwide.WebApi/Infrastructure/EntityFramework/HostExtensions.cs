using Microsoft.EntityFrameworkCore;
using MrWorldwide.WebApi.Data;

namespace MrWorldwide.WebApi.Infrastructure.EntityFramework;

public static class HostExtensions
{
    public static async Task MigrateDatabaseAsync(this IHost host)
    {
        var serviceProvider = host.Services;
        await using var scope = serviceProvider.CreateAsyncScope();
        await using var db = scope.ServiceProvider.GetRequiredService<MrWorldwideDbContext>();
        await db.Database.MigrateAsync();
    }
}