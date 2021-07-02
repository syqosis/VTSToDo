using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTSToDo.DAL;
using VTSToDo.Models;

namespace VTSToDo.Services
{
    public class ToDoListItemService : IToDoListItemService
    {
        private ToDoContext _context;

        public ToDoListItemService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteToDoListItem(int listId, int id)
        {
            var item = await _context.ToDoListItems.FirstOrDefaultAsync(x => x.ToDoListId == listId && x.Id == id);
            if (item == null)
            {
                return false;
            }

            _context.ToDoListItems.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ToDoListItemModel> GetToDoListItem(int listId, int id)
        {
            var result = await _context.ToDoListItems.FirstOrDefaultAsync(x => x.ToDoListId == listId && x.Id == id);
            if (result != null)
            {
                return new ToDoListItemModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    ToDoListId = result.ToDoListId,
                    IsComplete = result.IsComplete
                };
            }
            return null;
        }

        public async Task<List<ToDoListItemModel>> GetToDoListItems(int listId)
        {
            var results = _context.ToDoListItems.Where(x => x.ToDoListId == listId).Select(x => new ToDoListItemModel
            { 
                Id = x.Id,
                Name = x.Name
            });

            return await results.ToListAsync();
        }

        public async Task<ToDoListItemModel> CreateToDoListItem(ToDoListItemModel model)
        {
            var item = new ToDoListItem { 
                Name = model.Name,
                ToDoListId = model.ToDoListId,
                IsComplete = model.IsComplete
            };

            _context.ToDoListItems.Add(item);

            await _context.SaveChangesAsync();

            return new ToDoListItemModel
            { 
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete,
                ToDoListId = item.ToDoListId
            };
        }

        public async Task<ToDoListItemModel> UpdateToDoListItem(ToDoListItemModel model)
        {
            var item = await _context.ToDoListItems.FirstOrDefaultAsync(x => x.ToDoListId == model.ToDoListId && x.Id == model.Id);
            if (item == null)
            {
                return null;
            }

            item.Name = model.Name;
            item.IsComplete = model.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return null;
            }

            return new ToDoListItemModel
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete,
                ToDoListId = item.ToDoListId
            };
        }
    }
}
