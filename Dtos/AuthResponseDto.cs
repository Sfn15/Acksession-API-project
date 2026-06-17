namespace FinalProject.Dtos;

using System.ComponentModel.DataAnnotations;

public class AuthResponseDto
{
    [Required]
    public required string Token {get; set;}
    [Required]
    public required string Username {get; set;}
    public DateTime ExpiresAt{get; set;}

}