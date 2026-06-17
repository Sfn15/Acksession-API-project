namespace FinalProject.Dtos;

using System.ComponentModel.DataAnnotations;

public class CreateTaskDto
{
    [Required]
    public required string Title {get; set;}
    [Required]
    public required string Description{get; set;}
    [Required]
    public required int CategoryId {get; set;}
}