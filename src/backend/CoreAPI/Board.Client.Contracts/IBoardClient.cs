using Board.Client.Contracts.Resources;

namespace Board.Client.Contracts;

public interface IBoardClient
{
    public IBoardResource BoardResource { get; }
    
    public IBoardColumnResource BoardColumnResource { get; }
    
    public IBoardTypeResource BoardTypeResource { get; }
    
    public ITaskResource TaskResource { get; }
}
