using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTSToDo.DAL
{
    public class ToDoList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public virtual List<ToDoListItem> Items { get; set; }
    }
}
