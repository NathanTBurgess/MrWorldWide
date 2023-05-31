using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MrWorldwide.WebApi.Data;
using MrWorldwide.WebApi.Data.Entities;
using MrWorldwide.WebApi.Features.Authorization.Domain;

namespace MrWorldwide.Tests.IntegrationTests.Data;

public class IntegrationTestDataUtility
{
    private readonly MrWorldwideDbContext _dbContext;
    private readonly UserManager<AppUser> _userManager;

    public IntegrationTestDataUtility(MrWorldwideDbContext dbContext, UserManager<AppUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task InitializeDatabaseAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.MigrateAsync();
    }

    public async Task SetUpTestUsers()
    {
        var adminUser = TestAdmin.Create();
        await _userManager.CreateAsync(adminUser);
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(AppClaimTypes.Id, adminUser.Id),
            new Claim(AppClaimTypes.Email, adminUser.Email!),
            new Claim(AppClaimTypes.Name, adminUser.Name)
        });
        await _userManager.AddClaimsAsync(adminUser, identity.Claims);
    }

    public async Task TearDownAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
    }
}