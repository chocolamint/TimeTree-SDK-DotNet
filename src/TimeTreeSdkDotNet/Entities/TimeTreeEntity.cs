namespace TimeTreeSdkDotNet.Entities;

/// <summary>
/// Represents an entity on TimeTree API.
/// </summary>
/// <typeparam name="T">The type of an attributes.</typeparam>
/// <param name="Id">An identifier of the entity.</param>
/// <param name="Type">The entity type of this instance.</param>
/// <param name="Attributes">Additional information about this instance.</param>
public record TimeTreeEntity<T>(string Id, TimeTreeEntityType Type, T Attributes);
