using MrWorldwide.WebApi.Data.Entities;

namespace MrWorldwide.Tests.IntegrationTests.Data;

public class TestAdmin
{
    public const string Email = "totally.real+seriously@gmail.com";
    public const string Name = "Jeff RealPerson";

    public static AppUser Create() => new()
    {
        Email = Email,
        Name = Name,
        UserName = Email,
        EmailConfirmed = true
    };
}