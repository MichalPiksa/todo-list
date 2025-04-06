using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/todo")]
    public class ToDoController : ControllerBase
    {
        public ToDoController()
        {
        }

        public static List<ToDoItem> _toDoItems = new List<ToDoItem>
        {
            new ToDoItem()
            {
                Id = Guid.NewGuid(),
                Name = "TestPast",
                Deadline = DateTime.UtcNow,
                Description = "Description1",
                ToDoStatus = Status.Done
            },
            new ToDoItem()
            {
                Id = Guid.NewGuid(),
                Name = "TestFuture",
                Deadline = DateTime.UtcNow.AddDays(2),
                Description = "Description2",
                ToDoStatus = Status.New
            }
        };

        [HttpGet(Name = "FilterTodos")]
        public ActionResult<IEnumerable<ToDoItem>> GetTodosFilter(string? filter)
        {
            if (filter == "nearestTodo")
            {
                var nearestTodo = _toDoItems
                    .Where(x => x.Deadline > DateTime.UtcNow)
                    .OrderBy(x => x.Deadline)
                    .FirstOrDefault();
                return Ok(nearestTodo);
            }
            else if (filter == "allPassedTodos")
            {
                var allPassedTodos = _toDoItems
                    .Where(x => x.Deadline < DateTime.UtcNow);
                return Ok(allPassedTodos);
            }
            else if (filter == "AllTodos")
            {
                return Ok(_toDoItems);
            }
            else if (filter == "todayDeadlineTodos")
            {
                var todayDeadlineTodos = _toDoItems
                    .Where(x => x.Deadline == DateTime.UtcNow.Date)
                    .Where(x => x.Deadline < DateTime.Today.AddDays(1));
                return Ok(todayDeadlineTodos);
            }
            else if (filter == "actualWeekTasks") 
            {
                var restDaysTillEndOfWeek = DayOfWeek.Saturday + 1 - DateTime.Today.DayOfWeek;
                var actualWeekTasks = _toDoItems
                    .Where(x => x.Deadline > DateTime.UtcNow)
                    .Where(x => x.Deadline < DateTime.Today
                    .AddDays(restDaysTillEndOfWeek));
                return Ok(actualWeekTasks);
            }
            var actualTodos = _toDoItems.Where(x => x.Deadline > DateTime.Now);
            return Ok(actualTodos);
        }

        [HttpPost(Name = "AddTodo")]
        public IActionResult PostTodo([FromBody] ToDoItem item)
        {
            if (item.Deadline <= DateTime.UtcNow)
            {
                return BadRequest("Due date must be in the future.");
            }
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            //if (item.ToDoStatus != Status.New || item.ToDoStatus == null)
            //{
            //    item.ToDoStatus = Status.New;
            //}
            _toDoItems.Add(item);
            return CreatedAtAction(nameof(GetTodosFilter), new {id = item.Id}, item);
        }
    }
}
