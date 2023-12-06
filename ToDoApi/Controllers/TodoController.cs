using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Context;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoContext _context;
    public TodoController(TodoContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllTodos")]
    public async Task<ActionResult<List<Todo>>> Get(TodoContext context)
    {
        var todos = await context.Todos.ToListAsync();
        if (todos == null || todos.Count == 0)
        {
            return NotFound("Tarefas não encontradas");
        }
        return Ok(todos);
    }

    [HttpGet("GetTodoById/{id}", Name = "GetTodoById")]
    public async Task<ActionResult<Todo>> Get(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound("Tarefa não encontrada");
        }

        return Ok(todo);
    }

    [HttpPost("CreateTodo")]
    public async Task<ActionResult<Todo>> Post(Todo todo)
    {
        if (todo is null)
        {
            return BadRequest("A tareda não pode ser vazia");
        }

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTodoById", new { id = todo.Id }, todo);
    }

    [HttpPut("UpdateTodo")]
    public async Task<ActionResult<Todo>> Put(int id, Todo todo)
    {
        if (id != todo.Id)
        {
            return BadRequest("ID de tarefa não correspondente");
        }

        _context.Entry(todo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(todo);
    }

    [HttpDelete("DeleteTodo")]
    public async Task<ActionResult<Todo>> Delete(int id)
    {
        var todo = _context.Todos.FirstOrDefault(t => t.Id == id);

        if (todo is null)
        {
            return NotFound("Tareda não encontrada");
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return Ok(todo);
    }
}
