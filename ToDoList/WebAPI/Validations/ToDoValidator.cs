using System.Data;
using FluentValidation;
using ToDoListApp;

namespace WebAPI.Validations
{
    public class ToDoValidator : AbstractValidator<ToDoItem>
    {
        public ToDoValidator()
        {
            //RuleFor(x => x.Deadline)
            //    .Must(ValidFutureDate);
        }

        //public bool ValidFutureDate(DateTime date)
        //{
        //    return date > DateTime.Now;
        //}
    }
}
