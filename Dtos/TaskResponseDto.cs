namespace FinalProject.Dtos;

using System.ComponentModel.DataAnnotations;

public class TaskResponseDto
{
    [Required]
    public required int Id {get; set;}
    [Required]
    public required string Title{get; set;} = string.Empty;
    [Required]
    public required string Description{get; set;} = string.Empty;
    [Required]
    public required int CategoryId{get; set;}
}