namespace ToDoListApp.Tests
{
    public class ToDoListTests
    {
        public static List<ToDoItem> todosList;
        public ToDoListTests()
        {
            todosList = new List<ToDoItem>();
        }
        [Fact]
        public void ToDo_Succesfully_Created()
        {
            // Arrange
            var todo = new ToDoItem 
            {
                Name = "Test name",
                Description = "Test",
                Deadline = DateTime.Today.AddDays(2)
            };

            // Act
            todosList.Add(todo);

            // Assert
            Assert.Equal(todo.Id, todosList[0].Id);
            Assert.Equal("Test name", todosList[0].Name);
            Assert.Equal(1, todosList.Count);
        }
    }
}
