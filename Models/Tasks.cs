
namespace FinalProject.Models;
public class Tasks
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public int UserId {get; set;}
    public int CategoryId {get; set;}
}