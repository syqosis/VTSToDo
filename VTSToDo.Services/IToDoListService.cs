using System.Collections.Generic;
using System.Threading.Tasks;
using VTSToDo.Models;

namespace VTSToDo.Services
{
    public interface IToDoListService
    {
        public Task<List<ToDoListModel>> GetToDoLists(int userId);

        public Task<ToDoListModel> GetToDoList(int userId, int id);

        public Task<bool> DeleteToDoList(int userId, int id);

        public Task<ToDoListModel> CreateToDoList(int userId, ToDoListModel model);

        public Task<ToDoListModel> UpdateToDoList(int userId, ToDoListModel model);

    }
}
