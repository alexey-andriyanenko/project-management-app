namespace Board.Contracts.Exceptions;

public class BoardNotFoundException(Guid tenantId, Guid projectId, Guid boardId)
    : Exception($"Board with Id '{boardId}' in Project with Id '{projectId}' for Tenant with Id '{tenantId}' was not found.");