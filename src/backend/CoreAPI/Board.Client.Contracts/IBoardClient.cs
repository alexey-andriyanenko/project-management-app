using Board.Client.Contracts.Resources;

namespace Board.Client.Contracts;

public interface IBoardClient
{
    public IBoardResource BoardResource { get; }
    
    public ITaskResource TaskResource { get; }
}
