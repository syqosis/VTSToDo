using System.ComponentModel.DataAnnotations.Schema;

namespace VTSToDo.DAL
{
    public class ToDoListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ToDoListId { get; set; }

        public bool IsComplete { get; set; }

        [ForeignKey("ToDoListId")]
        public ToDoList ToDoList { get; set; }
    }
}
