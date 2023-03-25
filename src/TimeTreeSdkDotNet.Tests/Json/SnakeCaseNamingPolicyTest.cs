using TimeTreeSdkDotNet.Json;

namespace TimeTreeSdkDotNet.Tests.Json;

public class SnakeCaseNamingPolicyTest
{
    [Theory]
    [InlineData("Test", "test")]
    [InlineData("FooBar", "foo_bar")]
    public void CanConvertPropetyName(string pascalCaseName, string snakeCaseName)
    {
        var policy = new SnakeCaseNamingPolicy();
        Assert.Equal(snakeCaseName, policy.ConvertName(pascalCaseName));
    }
}
