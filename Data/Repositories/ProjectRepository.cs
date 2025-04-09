using Data.Contexts;
using Data.Entities;
using DomainModels = Domain.Models;

namespace Data.Repositories;


public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    Task GetAllAsync(bool orderByDescending, Func<object, object> sortBy, object where, Func<object, object> value1, Func<object, object> value2, Func<object, object> value3);
}

public class ProjectRepository(AppDbContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
}
