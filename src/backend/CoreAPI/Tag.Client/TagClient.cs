using Tag.Client.Contracts;
using Tag.Client.Contracts.Resources;

namespace Tag.Client;

public class TagClient(ITagResource tagResource) : ITagClient
{
    public ITagResource TagResource { get; } = tagResource;
}