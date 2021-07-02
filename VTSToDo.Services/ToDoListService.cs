using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTSToDo.DAL;
using VTSToDo.Models;

namespace VTSToDo.Services
{
    public class ToDoListService : IToDoListService
    {
        private ToDoContext _context;

        public ToDoListService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteToDoList(int userId, int id)
        {
            var item = await _context.ToDoLists.Include(x => x.Items).FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);
            if (item == null)
            {
                return false;
            }

            _context.ToDoListItems.RemoveRange(item.Items);
            _context.ToDoLists.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ToDoListModel> GetToDoList(int userId, int id)
        {
            var result = await _context.ToDoLists.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);
            if (result != null)
            {
                return new ToDoListModel
                {
                    Id = result.Id,
                    Name = result.Name
                };
            }
            return null;
        }

        public async Task<List<ToDoListModel>> GetToDoLists(int userId)
        {
            var results = _context.ToDoLists.Where(x => x.UserId == userId).Select(x => new ToDoListModel
            { 
                Id = x.Id,
                Name = x.Name
            });

            return await results.ToListAsync();
        }

        public async Task<ToDoListModel> CreateToDoList(int userId, ToDoListModel model)
        {
            var item = new ToDoList { 
                Name = model.Name,
                UserId = userId
            };

            _context.ToDoLists.Add(item);

            await _context.SaveChangesAsync();

            return new ToDoListModel { 
                Id = item.Id,
                Name = item.Name
            };
        }

        public async Task<ToDoListModel> UpdateToDoList(int userId, ToDoListModel model)
        {
            var item = await _context.ToDoLists.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == model.Id);
            if (item == null)
            {
                return null;
            }

            item.Name = model.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            return new ToDoListModel { 
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}
