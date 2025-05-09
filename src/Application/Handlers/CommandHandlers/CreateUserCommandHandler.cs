using Application.Commands.Users;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.CommandHandlers;

public class CreateUserCommandHandler
{
    private readonly IUserRepository _repository;
    private readonly ICacheService _cache;

    public CreateUserCommandHandler(IUserRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<string> HandleAsync(CreateUserCommand command)
    {
        var user = new User
        {
            Name = command.Name,
            Email = command.Email
        };

        await _repository.CreateAsync(user);
        await _cache.RemoveAsync("all_users");
        
        return user.Id;
    }
}