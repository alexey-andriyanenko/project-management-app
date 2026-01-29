namespace Board.Contracts.Exceptions;

public class DuplicateColumnNamesException(string duplicateColumnName) 
    : Exception($"Column name '{duplicateColumnName}' conflicts with a default column for this board type. Please use a different name.");
