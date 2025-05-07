using Application.Interfaces;
using Domain.Entities;
using SecondHandEcommerce.Application.Queries.Users;

namespace Application.Handlers.QueryHandlers;

public class GetUserByIdQueryHandler
{
    private readonly IUserRepository _repository;

    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User?> HandleAsync(GetUserByIdQuery query)
    {
        return await _repository.GetByIdAsync(query.UserId);
    }
}
