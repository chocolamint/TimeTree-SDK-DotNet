namespace TimeTreeSdkDotNet.Entities;

/// <summary>
/// Provides additional information about a TimeTree user.
/// </summary>
/// <param name="Name">The user name.</param>
/// <param name="Description">The user description.</param>
/// <param name="ImageUrl">The <see cref="Uri"/> of the user's profile image.</param>
public record TimeTreeUserAttribute(string Name, string Description, Uri ImageUrl);