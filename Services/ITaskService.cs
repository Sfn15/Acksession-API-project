using FinalProject.Dtos;
using FinalProject.Models;

namespace FinalProject.Services;

public interface ITaskService
{
    public Task<IEnumerable<Tasks>> GetAllAsyncAdmin();
    public Task<IEnumerable<TaskResponseDto>> GetAllAsyncUser(int userId);
    public Task<Tasks?> GetByIdAsync(int id);
    public Task<Tasks?> CreateAsync(CreateTaskDto dto, int userId);
    public Task<Tasks?> UpdateAsync(CreateTaskDto dto, int taskId);
    public Task<bool> DeleteAsync(int id);
}
