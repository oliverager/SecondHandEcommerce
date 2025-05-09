using Application.Commands.Listings;
using Application.Handlers.CommandHandlers;
using Application.Handlers.QueryHandlers;
using Application.Queries.Listings;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingController : ControllerBase
{
    private readonly CreateListingCommandHandler _createHandler;
    private readonly GetAllListingsQueryHandler _getAllHandler;
    private readonly GetListingByIdQueryHandler _getByIdHandler;

    public ListingController(
        CreateListingCommandHandler createHandler,
        GetAllListingsQueryHandler getAllHandler,
        GetListingByIdQueryHandler getByIdHandler)
    {
        _createHandler = createHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Listing>>> GetAll()
    {
        var listings = await _getAllHandler.HandleAsync(new GetAllListingsQuery());
        return Ok(listings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Listing>> GetById(string id)
    {
        var listing = await _getByIdHandler.HandleAsync(new GetListingByIdQuery { Id = id });
        if (listing == null)
            return NotFound();

        return Ok(listing);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateListingCommand command)
    {
        var id = await _createHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
}
