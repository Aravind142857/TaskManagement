// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using backend.Models;
// using System.Threading.Tasks;
// using backend.Data;
// using Microsoft.EntityFrameworkCore;
// namespace backend.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class TaskController : ControllerBase
//     {
//         private readonly AppDbContext _context;
//         public TaskController(AppDbContext context)
//         {
//             _context = context;
//         }
//         [HttpGet]
//         public async System.Threading.Tasks.Task<IActionResult> GetTasks()
//         {
//             var tasks = await _context.Tasks.ToListAsync();
//             return Ok(tasks);
//         }
//         [HttpPost]
//         public async System.Threading.Tasks.Task<IActionResult> CreateTask([FromBody] backend.Models.Task task)
//         {
//             task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);
//             _context.Tasks.Add(task);
//             await _context.SaveChangesAsync();
//             return CreatedAtAction(nameof(GetTasks), new {id = task.Id}, task);
//         }
//     }
// }