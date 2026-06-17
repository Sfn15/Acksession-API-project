using FinalProject.Models;
using FinalProject.Dtos;

namespace FinalProject.Services;

public class TaskService : ITaskService
{
    public static List<Tasks> _tasks = new List<Tasks>();

     public async Task<IEnumerable<Tasks>> GetAllAsyncAdmin()
    {
        return await Task.FromResult(_tasks);        
    }
    public async Task<IEnumerable<TaskResponseDto>> GetAllAsyncUser(int userId)
    {
        return await Task.FromResult(_tasks.Where(t => t.UserId == userId).Select(t => new TaskResponseDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CategoryId = t.CategoryId
        }));
    }
    public async Task<Tasks?> GetByIdAsync(int id)
    {
        return await Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
    }
    public async Task<Tasks?> CreateAsync(CreateTaskDto dto, int userId)
    {
        Tasks task = new Tasks
        {
            Id = _tasks.Count == 0 ? 1:_tasks.Max(t=>t.Id) + 1,
            Title = dto.Title,
            Description = dto.Description,
            UserId = userId,
            CategoryId = dto.CategoryId
        };

        _tasks.Add(task);

        return await Task.FromResult(task);
    }
    public async Task<Tasks?> UpdateAsync(CreateTaskDto dto, int taskId)
    {
        var target = _tasks.FirstOrDefault(p=> p.Id == taskId);
        if(target == null)
        {
            return null;
        }
        target.Title = dto.Title;
        target.Description = dto.Description;
        target.CategoryId = dto.CategoryId;
        return await Task.FromResult(target);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        Tasks? victim = _tasks.FirstOrDefault(t => t.Id == id);

        if(victim == null)
        {
            return false;
        }

        _tasks.Remove(victim);

        return true;
    }
}