using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Context;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    { }

    public DbSet<Todo> Todos { get; set; }


}
