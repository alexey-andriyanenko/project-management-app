using Board.Contracts.Parameters.Task;

namespace Facade.BoardManagement.Services.Mappings.Task;

public static class ParametersMappings
{
    public static GetTaskByIdParameters ToCoreParameters(
        this Contracts.Parameters.Task.GetTaskByIdParameters parameters)
    {
        return new GetTaskByIdParameters
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            BoardId = parameters.BoardId
        };
    }

    public static GetManyTasksByBoardIdParameters ToCoreParameters(
        this Contracts.Parameters.Task.GetManyTasksByBoardIdParameters parameters)
    {
        return new GetManyTasksByBoardIdParameters()
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            BoardId = parameters.BoardId
        };
    }

    public static CreateTaskParameters ToCoreParameters(
        this Contracts.Parameters.Task.CreateTaskParameters parameters)
    {
        return new CreateTaskParameters()
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            BoardId = parameters.BoardId,
            BoardColumnId = parameters.BoardColumnId,
            CreatorUserId = parameters.CreatorUserId,
            AssigneeUserId = parameters.AssigneeUserId,
            Title = parameters.Title,
            DescriptionAsJson = parameters.DescriptionAsJson,
            DescriptionAsPlainText = parameters.DescriptionAsPlainText,
            TagIds = parameters.TagIds.Select(tagId => tagId).ToList()
        };
    }
    
    public static UpdateTaskParameters ToCoreParameters(
        this Contracts.Parameters.Task.UpdateTaskParameters parameters)
    {
        return new UpdateTaskParameters()
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            BoardId = parameters.BoardId,
            BoardColumnId = parameters.BoardColumnId,
            AssigneeUserId = parameters.AssigneeUserId,
            Title = parameters.Title,
            DescriptionAsJson = parameters.DescriptionAsJson,
            DescriptionAsPlainText = parameters.DescriptionAsPlainText,
            TagIds = parameters.TagIds.Select(tagId => tagId).ToList()
        };
    }
    
    public static DeleteTaskParameters ToCoreParameters(
        this Contracts.Parameters.Task.DeleteTaskParameters parameters)
    {
        return new DeleteTaskParameters
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            BoardId = parameters.BoardId
        };
    }
}