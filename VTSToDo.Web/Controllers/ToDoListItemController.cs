using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTSToDo.Models;
using VTSToDo.Services;

namespace VTSToDo.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToDoListItemController : ControllerBase
    {
        private IToDoListItemService _ToDoListItemService;

        public ToDoListItemController(IToDoListItemService ToDoListItemService)
        {
            _ToDoListItemService = ToDoListItemService;
        }

        [HttpGet("{listId}")]
        public async Task<ActionResult<IEnumerable<ToDoListItemModel>>> GetToDoListItems(int listId)
        {
            try
            {
                return await _ToDoListItemService.GetToDoListItems(listId);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("{listId}/{id}")]
        public async Task<ActionResult<ToDoListItemModel>> GetToDoListItem(int listId, int id)
        {
            try
            {
                var result = await _ToDoListItemService.GetToDoListItem(listId, id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoListItem(int id, ToDoListItemModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }

                var result = await _ToDoListItemService.UpdateToDoListItem(model);
                if (result == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ToDoListItemModel>> CreateToDoListItem(ToDoListItemModel model)
        {
            try
            {
                var result = await _ToDoListItemService.CreateToDoListItem(model);

                return CreatedAtAction(nameof(GetToDoListItem), new { listId = result.ToDoListId, id = result.Id }, result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{listId}/{id}")]
        public async Task<IActionResult> DeleteToDoListItem(int listId, int id)
        {
            try
            {
                var result = await _ToDoListItemService.DeleteToDoListItem(listId, id);
                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
