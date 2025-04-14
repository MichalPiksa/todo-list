using System.Xml.Linq;
using Marten;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/todo")]
    public class ToDoController : ControllerBase
    {
        private readonly IDocumentSession _documentSession;
        public ToDoController(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        // public static List<ToDoItem> _toDoItems = new List<ToDoItem>
        // {
        //     new ToDoItem()
        //     {
        //         Id = Guid.NewGuid(),
        //         Name = "TestPast",
        //         Deadline = DateTime.UtcNow,
        //         Description = "Description1",
        //         ToDoStatus = Status.Done
        //     },
        //     new ToDoItem()
        //     {
        //         Id = Guid.NewGuid(),
        //         Name = "TestFuture",
        //         Deadline = DateTime.UtcNow.AddDays(2),
        //         Description = "Description2",
        //         ToDoStatus = Status.New
        //     }
        // };

        [HttpGet(Name = "FilterTodos")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodosFilter(string? filter)
        {
            var todos = await _documentSession.Query<ToDoItem>().ToListAsync();
            if (filter == "nearestTodo")
            {
                var nearestTodo = todos
                    .Where(x => x.Deadline > DateTime.UtcNow)
                    .OrderBy(x => x.Deadline)
                    .FirstOrDefault();
                return Ok(nearestTodo);
            }
            else if (filter == "allPassedTodos")
            {
                var allPassedTodos = todos
                    .Where(x => x.Deadline < DateTime.UtcNow);
                return Ok(allPassedTodos);
            }
            else if (filter == "AllTodos")
            {
                return Ok(todos);
            }
            else if (filter == "todayDeadlineTodos")
            {
                var todayDeadlineTodos = todos
                    .Where(x => x.Deadline == DateTime.UtcNow.Date)
                    .Where(x => x.Deadline < DateTime.Today.AddDays(1));
                return Ok(todayDeadlineTodos);
            }
            else if (filter == "actualWeekTasks") 
            {
                var restDaysTillEndOfWeek = DayOfWeek.Saturday + 1 - DateTime.Today.DayOfWeek;
                var actualWeekTasks = todos
                    .Where(x => x.Deadline > DateTime.UtcNow)
                    .Where(x => x.Deadline < DateTime.Today
                    .AddDays(restDaysTillEndOfWeek));
                return Ok(actualWeekTasks);
            }
            var actualTodos = todos.Where(x => x.Deadline > DateTime.Now);
            return Ok(actualTodos);
        }

        [HttpPost(Name = "AddTodo")]
        public async Task<IActionResult> PostTodo([FromBody] ToDoItem item)
        {
            if (item.Deadline <= DateTime.UtcNow)
            {
                return BadRequest("Due date must be in the future.");
            }
            if (item == null || string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Deadline.ToString()))
            {
                return BadRequest("Invalid todo item data.");
            }
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }

            var todo = new ToDoItem()
            {
                Id = item.Id,
                Name = item.Name,
                Deadline = item.Deadline,
                Description = item.Description,
                ToDoStatus = Status.New
            };
            try
            {
                _documentSession.Store(todo);
                await _documentSession.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving the todo item.");
            }
            return CreatedAtAction(nameof(PostTodo), new {id = item.Id}, item);
        }
    }
}
