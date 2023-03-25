using System.Text.Json;
using System.Text.RegularExpressions;

namespace TimeTreeSdkDotNet.Json;

internal class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    private static readonly Regex _regex = new("(.)([A-Z])");

    public override string ConvertName(string name)
        => _regex.Replace(name, x => x.Groups[1].Value + "_" + x.Groups[2].Value).ToLowerInvariant();
}