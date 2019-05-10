using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todoItems = new List<TodoItem>();

        /// <summary>
        /// Gets a list of all todo items.
        /// </summary>
        /// <returns>list of all todo items</returns>
        [ProducesResponseType(200)]  
        [HttpGet]       
        public IActionResult Get()
        {
            return Ok(_todoItems);
        }

        /// <summary>
        /// Gets a todo item by its id.
        /// </summary>
        /// <param name="id">id of the todo item</param>
        /// <returns>todo item with corresponding id</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var todoItem = GetTodoItem(id);

            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }

        [HttpPost]
        public IActionResult Add(TodoItemDto todoItemDto)
        {
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid().ToString(),
                Content = todoItemDto.Content,
                IsDone = todoItemDto.IsDone,
                TimeStamp = DateTime.Now
            };

            _todoItems.Add(todoItem);

            return Ok(todoItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todoItem = GetTodoItem(id);

            if (todoItem == null)
                return NotFound();

            _todoItems.Remove(todoItem);

            return Ok();
        }

        [HttpPut("status/{id}")]
        public IActionResult Update(string id)
        {
            var todoItem = GetTodoItem(id);

            if (todoItem == null)
                return NotFound();

            todoItem.IsDone = !todoItem.IsDone;
            todoItem.TimeStamp = DateTime.Now;

            return Ok(todoItem);
        }

        private TodoItem GetTodoItem(string id)
        {
            return _todoItems.FirstOrDefault(t => t.Id == id);
        }
    }
}