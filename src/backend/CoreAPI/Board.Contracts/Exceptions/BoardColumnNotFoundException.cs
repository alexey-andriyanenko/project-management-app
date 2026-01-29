namespace Board.Contracts.Exceptions;

public class BoardColumnNotFoundException(Guid tenantId, Guid projectId, Guid boardId, Guid boardColumnId)
    : Exception($"Board Column with Id '{boardColumnId}' for Board with Id '{boardId}' in Project with Id '{projectId}' for Tenant with Id '{tenantId}' was not found.");
    