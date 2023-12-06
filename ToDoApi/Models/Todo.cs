using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApi.Models;

[Table("Todos")]
public class Todo
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public bool IsComplete { get; set; }

    public DateTime CreatedAt { get; set; }
}

