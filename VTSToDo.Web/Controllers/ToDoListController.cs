using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTSToDo.Models;
using VTSToDo.Services;
using VTSToDo.Shared.Extensions;

namespace VTSToDo.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        private IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListModel>>> GetToDoLists()
        {
            try
            {
                return await _toDoListService.GetToDoLists(this.User.GetUserId());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListModel>> GetToDoList(int id)
        {
            try
            {
                var result = await _toDoListService.GetToDoList(this.User.GetUserId(), id);
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
        public async Task<IActionResult> UpdateTodoList(long id, ToDoListModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }

                var result = await _toDoListService.UpdateToDoList(this.User.GetUserId(), model);
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
        public async Task<ActionResult<ToDoListModel>> CreateTodoList(ToDoListModel model)
        {
            try
            {
                var result = await _toDoListService.CreateToDoList(this.User.GetUserId(), model);

                return CreatedAtAction(nameof(GetToDoList), new { id = result.Id }, result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            try
            {
                var result = await _toDoListService.DeleteToDoList(this.User.GetUserId(), id);
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
