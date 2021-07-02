using System.Collections.Generic;

namespace VTSToDo.DAL
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual List<ToDoList> ToDoLists { get; set; }
    }
}
