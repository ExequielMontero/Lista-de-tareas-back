using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_LIsta_de_tareas.Data;
using Api_LIsta_de_tareas.Models;

namespace Api_LIsta_de_tareas.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTask([FromBody] TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("El título es requerido");
            }

            var newTask = new TaskItem
            {
                Title = task.Title,
                IsCompleted = false
                // NO asignar el Id manualmente
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
        }

        // PUT: api/Tasks/5/toggle
        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
