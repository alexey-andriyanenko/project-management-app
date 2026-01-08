using Board.Client.Contracts;
using Board.Client.Contracts.Resources;

namespace Board.Client;

public class BoardClient(ITaskResource taskResource, IBoardResource boardResource) : IBoardClient
{
    public IBoardResource BoardResource { get; } = boardResource;

    public ITaskResource TaskResource { get; } = taskResource;
}
