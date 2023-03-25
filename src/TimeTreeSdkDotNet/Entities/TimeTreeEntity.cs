namespace TimeTreeSdkDotNet.Entities;

public record TimeTreeEntity<T>(string Id, TimeTreeEntityType Type, T Attributes);
