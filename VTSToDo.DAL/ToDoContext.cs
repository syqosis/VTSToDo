using Microsoft.EntityFrameworkCore;

namespace VTSToDo.DAL
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<ToDoList> ToDoLists { get; set; }

        public DbSet<ToDoListItem> ToDoListItems { get; set; }
    }
}
