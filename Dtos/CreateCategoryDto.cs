namespace FinalProject.Dtos;

using System.ComponentModel.DataAnnotations;

public class CreateCategoryDto
{
    [Required]
    public required string Title {get; set;}
}