namespace Tag.Contracts.Exceptions;

public class TagNotFoundException(Guid tagId) : Exception($"Tag with Id '{tagId}' was not found.")
{
    
}