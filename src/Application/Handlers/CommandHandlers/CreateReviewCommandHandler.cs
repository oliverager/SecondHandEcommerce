using Application.Commands.Reviews;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.CommandHandlers;

public class CreateReviewCommandHandler
{
    private readonly IReviewRepository _reviewRepo;
    private readonly IUserRepository _userRepo;

    public CreateReviewCommandHandler(IReviewRepository reviewRepo, IUserRepository userRepo)
    {
        _reviewRepo = reviewRepo;
        _userRepo = userRepo;
    }

    public async Task<string> HandleAsync(CreateReviewCommand command)
    {
        var review = new Review
        {
            Text = command.Text,
            Rating = command.Rating,
            SellerId = command.SellerId
        };

        // Save the review
        await _reviewRepo.CreateAsync(review);

        // Update user's ReviewIds list
        var user = await _userRepo.GetByIdAsync(command.SellerId);
        if (user != null)
        {
            user.ReviewIds.Add(review.Id);

            // Recalculate average rating
            var reviews = await _reviewRepo.GetAllAsync();
            var sellerReviews = reviews.Where(r => r.SellerId == command.SellerId).ToList();
            user.Rating = sellerReviews.Average(r => r.Rating);

            await _userRepo.UpdateAsync(user);
        }

        return review.Id;
    }
}
