using TimeTreeSdkDotNet.Entities;

namespace TimeTreeSdkDotNet.Tests;

public class TimeTreeClientTest
{
    [Fact]
    public void CanCreateInstance()
    {
        var ex = Record.Exception(() => CreateTimeTreeClient());
        Assert.Null(ex);
    }

    [Fact]
    public async Task CanGetUser()
    {
        var client = CreateTimeTreeClient();
        var expectedUserId = Environment.GetEnvironmentVariable("UserId")!;
        var expectedUserName = Environment.GetEnvironmentVariable("UserName")!;
        var expectedUserDescription = Environment.GetEnvironmentVariable("UserDescription")!;
        var expectedUserImageUrl = new Uri(Environment.GetEnvironmentVariable("UserImageUrl")!);

        var user = await client.GetUserAsync(CancellationToken.None);

        Assert.Equal(new(expectedUserId, TimeTreeEntityType.User,
            new(expectedUserName, expectedUserDescription, expectedUserImageUrl)),
            user);
    }

    [Fact]
    public async Task CanGetCalendars()
    {
        var client = CreateTimeTreeClient();

        var calendars = (await client.GetCalendarsAsync(CancellationToken.None)).ToArray();

        Assert.NotNull(calendars);
        Assert.NotEqual(0, calendars.Length);
        Assert.Multiple(
            () => Assert.Matches(".+", calendars[0].Id),
            () => Assert.Equal(TimeTreeEntityType.Calendar, calendars[0].Type),
            () => Assert.NotNull(calendars[0].Attributes),
            () => Assert.Matches(".+", calendars[0].Attributes.Name),
            () => Assert.NotNull(calendars[0].Attributes.Description),
            () => Assert.Matches("#[0-9a-fA-F]{6}", calendars[0].Attributes.Color),
            () => Assert.NotEqual(default(DateTimeOffset), calendars[0].Attributes.CreatedAt)
        );
        Assert.Equal(Enumerable.Range(0, calendars.Length), calendars.Select(x => x.Attributes.Order));
    }

    private static TimeTreeClient CreateTimeTreeClient()
    {
        var personnelAccessToken = Environment.GetEnvironmentVariable("PersonnelAccessToken")!;
        return new TimeTreeClient(personnelAccessToken);
    }
}