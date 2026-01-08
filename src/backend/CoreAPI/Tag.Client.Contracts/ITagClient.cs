using Tag.Client.Contracts.Resources;

namespace Tag.Client.Contracts;

public interface ITagClient
{
    public ITagResource TagResource { get; }
}