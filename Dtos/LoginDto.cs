namespace FinalProject.Dtos;

using System.ComponentModel.DataAnnotations;
public class LoginDto
{
    [Required]
    [EmailAddress]
    public required string email {get; set;}
    [Required]
    [StringLength(64,MinimumLength = 8)]
    public required string password{get; set;}


}