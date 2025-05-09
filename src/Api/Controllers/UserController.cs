using Application.Commands.Users;
using Application.Handlers.CommandHandlers;
using Application.Handlers.QueryHandlers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SecondHandEcommerce.Application.Queries.Users;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly CreateUserCommandHandler _createHandler;
    private readonly GetUserByIdQueryHandler _getByIdHandler;

    public UserController(CreateUserCommandHandler createHandler, GetUserByIdQueryHandler getByIdHandler)
    {
        _createHandler = createHandler;
        _getByIdHandler = getByIdHandler;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateUserCommand command)
    {
        var id = await _createHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(string id)
    {
        var user = await _getByIdHandler.HandleAsync(new GetUserByIdQuery { UserId = id });
        return user == null ? NotFound() : Ok(user);
    }
}
