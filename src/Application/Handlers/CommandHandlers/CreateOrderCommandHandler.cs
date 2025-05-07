using Application.Commands.Orders;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.CommandHandlers;

public class CreateOrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IListingRepository _listingRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepo, IListingRepository listingRepo)
    {
        _orderRepository = orderRepo;
        _listingRepository = listingRepo;
    }

    public async Task<string> HandleAsync(CreateOrderCommand command)
    {
        var listing = await _listingRepository.GetByIdAsync(command.ItemId);
        if (listing is null || listing.Status != ListingStatus.Available)
            throw new InvalidOperationException("Item not available");

        listing.Status = ListingStatus.Reserved;
        await _listingRepository.UpdateAsync(listing);

        var order = new Order
        {
            BuyerId = command.BuyerId,
            ItemId = command.ItemId,
            Status = OrderStatus.Pending
        };

        await _orderRepository.CreateAsync(order);
        return order.Id;
    }
}
