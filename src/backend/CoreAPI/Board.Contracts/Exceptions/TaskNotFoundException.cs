namespace Board.Contracts.Exceptions;

public class TaskNotFoundException(Guid taskId, Guid tenantId) : Exception($"Task with Id '{taskId}' for Tenant '{tenantId}' was not found.");