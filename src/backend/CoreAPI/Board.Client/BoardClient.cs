using Board.Client.Contracts;
using Board.Client.Contracts.Resources;

namespace Board.Client;

public class BoardClient(ITaskResource taskResource, IBoardResource boardResource, IBoardColumnResource boardColumnResource, IBoardTypeResource boardTypeResource) : IBoardClient
{
    public IBoardResource BoardResource { get; } = boardResource;
    
    public IBoardColumnResource BoardColumnResource { get; } = boardColumnResource;

    public IBoardTypeResource BoardTypeResource { get; } = boardTypeResource;
    
    public ITaskResource TaskResource { get; } = taskResource;
}
