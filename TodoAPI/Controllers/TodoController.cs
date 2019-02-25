using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context;
        }

        //GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        // POST api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = todoItem.Id }, todoItem);
        }

        // PUT api/Todo/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> Put(long id, TodoItem todoItem)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.Name = todoItem.Name;
            todo.IsComplete = todoItem.IsComplete;
            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync();
            return Ok(todo);
        }

        // DELETE api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> Delete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        //GET: api/Todo/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Search([FromQuery] string q)
        {
            return await _context.TodoItems
                .Where(x => x.Name.Contains(q))
                .ToListAsync();
        }

        
        //GET: api/Todo/CompletedTask
        [HttpGet("CompletedTask")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> CompletedTask()
        {
            return await _context.TodoItems
               .Where(x => x.IsComplete==true)
               .ToListAsync();
        }
    }
}
