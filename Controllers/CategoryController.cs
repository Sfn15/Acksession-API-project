using Microsoft.AspNetCore.Mvc;
using FinalProject.Dtos;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FinalProject.Models;

namespace FinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _categoryService.GetAllAsync());
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        Category? category = await _categoryService.GetByIdAsync(id);
        if(category == null)
        {
            return NotFound();
        }

        return Ok(category);

    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Category>> Create(CreateCategoryDto dto)
    {
        Category? created = await _categoryService.CreateAsync(dto);
        
        if(created == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetById), new {id = created.Id},created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{categoryId:int}")]
    public async Task<ActionResult<Category>> Update(CreateCategoryDto dto, int categoryId)
    {
    
        Category? response = await _categoryService.UpdateAsync(dto, categoryId);

        if(response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{categoryId:int}")]
    public async Task<ActionResult> Delete(int categoryId)
    {    
        bool success = await _categoryService.DeleteAsync(categoryId);

        if (success)
        {
            return Ok();
        }
        return NotFound();
    }
}