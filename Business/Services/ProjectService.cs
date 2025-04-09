using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Project = Domain.Models.Project;

namespace Business.Services;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
    Task<ProjectResult<IEnumerable<Project>>> GetProjectAsync();
    Task<ProjectResult<Project>> GetProjectAsync(string id);
}

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = StatusService;


    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        if (formData == null)
            return new ProjectResult { Succeded = false, StatusCode = 400, Error = "Not all required field are supplied." };

        var projectEntity = formData.MapTo<ProjectEntity>();
        var status = await _statusService.GetStatusByIdAsync(1);

        projectEntity.StatusId = status!.Id;

        var result = await _projectRepository.AddAsync(projectEntity);

        return result.Succeded
            ? new ProjectResult { Succeded = true, StatusCode = 201 }
            : new ProjectResult { Succeded = false, StatusCode = result.StatusCode, Error = result.Error }

    }


    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectAsync()
    {
        var response = await _projectRepository.GetAllAsync
            (
                orderByDescending: true,
                sortBy: s => s.Created,
                where: null,
                include => include.User,
                include => include.Status,
                include => include.Client
            );

        return new ProjectResult<IEnumerable<Project>> { Succeded = true, StatusCode = 200, Result = response.Result };
    }



    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
            (
                where: x => x.Id == id,
                include => include.User,
                include => include.Status,
                include => include.Client
            );
        return response.Succeded
            ? new ProjectResult<Project> { Succeded = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Project> { Succeded = false, StatusCode = 404, Error = $"Project '{id}' was not found." };
    }
}  
