using System.Net;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Api;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<IdentityUser> _userManager;

    public TodoItemsController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
    {
        _todoItemService = todoItemService;
        _userManager = userManager;

    }
    
    [HttpGet]
     public async Task<ActionResult> GetAll()
    {
        var user = await _userManager.GetUserAsync(User);
        if( user == null)
        {
            return Challenge();
        }
        var items = _todoItemService.GetIncompleteItemsAsync(user);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userManager.GetUserAsync(User);
        if( user == null)
        {
            return Challenge();
        }
        var items = await _todoItemService.GetIncompleteItemsAsync(user);
        var item = items.FirstOrDefault(val => val.Id == id);

        if(item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
    [HttpPost("completed/{id}")]
    public async Task<IActionResult> MarkCompleted(Guid id)
    {
         var user = await _userManager.GetUserAsync(User);
        if( user == null)
        {
            return Challenge();
        }
        var result = await _todoItemService.MarkDoneAsync(id, user);
        if(result)
        {
            return Ok();
        }

        return NotFound();

    }

    /*
    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody]TodoItem value)
    {
        var user = await _userManager.GetUserAsync(User);
        if(user == null)
        {
            return Challenge();
        }

        return Ok();

    }
    */
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(Guid id, [FromBody]TodoItem value)
    {

        if (id != value.Id)
        {
            ModelState.AddModelError("Id", "Mismatch");

        }

        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.GetUserAsync(User);
        if(user == null)
        {
            return Challenge();
        }

        return StatusCode((int)HttpStatusCode.NotImplemented);       
    }
}