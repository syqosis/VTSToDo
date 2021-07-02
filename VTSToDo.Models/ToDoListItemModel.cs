namespace VTSToDo.Models
{
    public class ToDoListItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ToDoListId { get; set; }

        public bool IsComplete { get; set; }
    }
}
