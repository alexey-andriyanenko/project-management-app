namespace Board.Contracts.Exceptions;

public class BoardNotFoundException(Guid boardId, Guid tenantId): Exception($"Board with Id '{boardId}' for Tenant with Id '{tenantId}' was not found.");