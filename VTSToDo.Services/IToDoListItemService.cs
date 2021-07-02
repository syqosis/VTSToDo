using System.Collections.Generic;
using System.Threading.Tasks;
using VTSToDo.Models;

namespace VTSToDo.Services
{
    public interface IToDoListItemService
    {
        public Task<List<ToDoListItemModel>> GetToDoListItems(int listId);

        public Task<ToDoListItemModel> GetToDoListItem(int listId, int id);

        public Task<bool> DeleteToDoListItem(int listId, int id);

        public Task<ToDoListItemModel> CreateToDoListItem(ToDoListItemModel model);

        public Task<ToDoListItemModel> UpdateToDoListItem(ToDoListItemModel model);
    }
}
