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

    private static TimeTreeClient CreateTimeTreeClient()
    {
        var personnelAccessToken = Environment.GetEnvironmentVariable("PersonnelAccessToken")!;
        return new TimeTreeClient(personnelAccessToken);
    }
}