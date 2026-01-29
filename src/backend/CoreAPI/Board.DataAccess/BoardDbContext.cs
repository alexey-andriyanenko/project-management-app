using Board.DataAccess.Configurations;
using Board.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Board.DataAccess;

public class BoardDbContext(DbContextOptions<BoardDbContext> options) : DbContext(options)
{
    public DbSet<BoardEntity> Boards { get; set; }

    public DbSet<BoardColumnEntity> BoardColumns { get; set; }

    public DbSet<BoardTypeEntity> BoardTypes { get; set; }
    
    public DbSet<TaskEntity> Tasks { get; set; }
    
    public DbSet<TaskCommentEntity> TaskComments { get; set; }
    
    public DbSet<TaskTagEntity> TaskTags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BoardEntityTypeConfiguration());
        builder.ApplyConfiguration(new BoardColumnEntityTypeConfiguration());
        builder.ApplyConfiguration(new BoardTypeEntityTypeConfiguration());
        builder.ApplyConfiguration(new TaskEntityTypeConfiguration());
        builder.ApplyConfiguration(new TaskCommentEntityTypeConfiguration());
        builder.ApplyConfiguration(new TaskTagEntityTypeConfiguration());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding((context, _) =>
        {
            var boardTypesContext = context.Set<BoardTypeEntity>();

            if (boardTypesContext.Any())
            {
                return;
            }
            
            var defaultBoardTypes = new List<BoardTypeEntity>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kanban",
                    IsEssential = true,
                    TenantId = Guid.Empty
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Scrum",
                    IsEssential = true,
                    TenantId = Guid.Empty
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Backlog",
                    IsEssential = true,
                    TenantId = Guid.Empty
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Custom",
                    IsEssential = false,
                    TenantId = Guid.Empty
                }
            };
        });
        
        base.OnConfiguring(optionsBuilder);
    }
}