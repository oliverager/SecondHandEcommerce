using Application.Interfaces;
using Application.Queries.Reviews;
using Domain.Entities;

namespace Application.Handlers.QueryHandlers;

public class GetReviewsBySellerIdQueryHandler
{
    private readonly IReviewRepository _repository;

    public GetReviewsBySellerIdQueryHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Review>> HandleAsync(GetReviewsBySellerIdQuery query)
    {
        var allReviews = await _repository.GetAllAsync();
        return allReviews.Where(r => r.SellerId == query.SellerId);
    }
}