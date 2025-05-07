using Application.Interfaces;
using Domain.Entities;
using SecondHandEcommerce.Application.Commands.Reviews;

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
            SellerId = command.SellerId,
            Text = command.Text,
            Rating = command.Rating
        };

        await _reviewRepo.CreateAsync(review);

        var seller = await _userRepo.GetByIdAsync(command.SellerId);
        if (seller != null)
        {
            seller.ReviewIds.Add(review.Id);
            // TODO: optionally recalculate seller's average rating
            await _userRepo.UpdateAsync(seller);
        }

        return review.Id;
    }
}
