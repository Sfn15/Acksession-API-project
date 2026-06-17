using FinalProject.Models;
using FinalProject.Dtos;

namespace FinalProject.Services;
public class CategoryService : ICategoryService
{
    private static List<Category> _categories = new List<Category>();
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await Task.FromResult(_categories);
    }
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await Task.FromResult(_categories.FirstOrDefault(c => c.Id == id));
    }
    public async Task<Category?> CreateAsync(CreateCategoryDto dto)
    {
        Category category = new Category
        {
            Id = _categories.Count() == 0 ? 1 : _categories.Max(c => c.Id) + 1,
            Title = dto.Title
        };
        _categories.Add(category);

        return await Task.FromResult(category);
    }
    public async Task<Category?> UpdateAsync(CreateCategoryDto dto, int categoryId)
    {
        Category? target = _categories.FirstOrDefault(c => c.Id == categoryId);
        if(target == null)
        {
            return null;
        }
        target.Title = dto.Title;
        return target;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        Category? victim = _categories.FirstOrDefault(c => c.Id == id);
        
        if(victim == null)
        {
            return false;
        }

        _categories.Remove(victim);
        return true;
    }
}