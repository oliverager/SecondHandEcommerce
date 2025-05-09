using Application.Commands.Reviews;
using Application.Handlers.CommandHandlers;
using Application.Handlers.QueryHandlers;
using Application.Queries.Reviews;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly CreateReviewCommandHandler _handler;
    private readonly GetReviewByIdQueryHandler _getByIdHandler;
    private readonly GetReviewsBySellerIdQueryHandler _getBySellerHandler;

    public ReviewController(
        CreateReviewCommandHandler handler,
        GetReviewByIdQueryHandler getByIdHandler,
        GetReviewsBySellerIdQueryHandler getBySellerHandler)
    {
        _handler = handler;
        _getByIdHandler = getByIdHandler;
        _getBySellerHandler = getBySellerHandler;
    }


    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateReviewCommand command)
    {
        var id = await _handler.HandleAsync(command);
        return CreatedAtAction(null, new { id }, id);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetById(string id)
    {
        var review = await _getByIdHandler.HandleAsync(new GetReviewByIdQuery { ReviewId = id });
        return review == null ? NotFound() : Ok(review);
    }
    
    [HttpGet("seller/{sellerId}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetBySellerId(string sellerId)
    {
        var reviews = await _getBySellerHandler.HandleAsync(new GetReviewsBySellerIdQuery { SellerId = sellerId });
        return Ok(reviews);
    }

}
