using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage = "Empty string not allowed"), MinLength(3)]
        public required string Name { get; set; }
        public required DateTime Deadline { get; set; }
        public string Description { get; set; }
        public Status ToDoStatus { get; set; }
    }
}
