using FinalProject.Dtos;
using FinalProject.Models;

namespace FinalProject.Services;

public interface ICategoryService
{
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task<Category?> GetByIdAsync(int id);
    public Task<Category?> CreateAsync(CreateCategoryDto dto);
    public Task<Category?> UpdateAsync(CreateCategoryDto dto, int categoryId);
    public Task<bool> DeleteAsync(int id);
}