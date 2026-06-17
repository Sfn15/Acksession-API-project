using Microsoft.AspNetCore.Mvc;
using FinalProject.Dtos;
using FinalProject.Services;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
        {
            return Unauthorized();
        }
        bool isAdmin = User.IsInRole("Admin");

        if (isAdmin)
        {
            IEnumerable<Tasks> all = await _taskService.GetAllAsyncAdmin();
            return Ok(all);
        }
        IEnumerable<TaskResponseDto> tasks = await _taskService.GetAllAsyncUser(id);
        return Ok(tasks);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        Tasks? task = await _taskService.GetByIdAsync(id);
        if(task == null)
        {
            return NotFound();
        }

        bool isAdmin = User.IsInRole("Admin");

        if (isAdmin)
        {
            return Ok(task);
        }

        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized();
        }

        if(userId == task.UserId)
        {
            TaskResponseDto response = new TaskResponseDto
            {
              Id = task.Id,
              Title = task.Title,
              Description = task.Description,
              CategoryId = task.CategoryId
            };
            return Ok(response);
        }
        return Forbid();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Tasks>> Create(CreateTaskDto dto)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
        {
            return Unauthorized();
        }
        Tasks? created = await _taskService.CreateAsync(dto, id);
        
        if(created == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetById), new {id = created.Id},created);
    }

    [Authorize]
    [HttpPut("{taskId:int}")]
    public async Task<ActionResult<Tasks>> Update(CreateTaskDto dto, int taskId)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized();
        }

        Tasks? target = await _taskService.GetByIdAsync(taskId);

        if(target == null)
        {
            return NotFound();
        }

        bool isAdmin = User.IsInRole("Admin");

        if (isAdmin || userId == target.UserId)
        {
            Tasks? response = await _taskService.UpdateAsync(dto, taskId);

            if(response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
        return Forbid();
    }

    [Authorize]
    [HttpDelete("{taskId:int}")]
    public async Task<ActionResult> Delete(int taskId)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            return Unauthorized();
        }
        bool isAdmin = User.IsInRole("Admin");

        Tasks? victim = await _taskService.GetByIdAsync(taskId);

        if(victim == null)
        {
            return NotFound();
        }

        if(isAdmin || victim.UserId == userId)
        {
            bool success = await _taskService.DeleteAsync(taskId);

            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
        return Forbid();
    }
}

