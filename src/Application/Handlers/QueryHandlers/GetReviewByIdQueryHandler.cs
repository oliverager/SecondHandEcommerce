using Application.Interfaces;
using Application.Queries.Reviews;
using Domain.Entities;

namespace Application.Handlers.QueryHandlers;

public class GetReviewByIdQueryHandler
{
    private readonly IReviewRepository _repository;

    public GetReviewByIdQueryHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<Review?> HandleAsync(GetReviewByIdQuery query)
    {
        return await _repository.GetByIdAsync(query.ReviewId);
    }
}