using Application.Interfaces;
using Domain.Entities;
using SecondHandEcommerce.Application.Queries.Users;


namespace Application.Handlers.QueryHandlers
{
    public class GetUserByIdQueryHandler
    {
        private readonly IUserRepository _repository;
        private readonly ICacheService _cache;  // Inject CacheService

        public GetUserByIdQueryHandler(IUserRepository repository, ICacheService cache)
        {
            _repository = repository;
            _cache = cache;  // Assign CacheService
        }

        public async Task<User?> HandleAsync(GetUserByIdQuery query)
        {
            string cacheKey = $"user:{query.UserId}";  // Cache key for the user

            // Check if the user is cached
            var cachedUser = await _cache.GetAsync<User>(cacheKey);
            if (cachedUser != null)
            {
                return cachedUser;  // Return cached user if available
            }

            // If not cached, get it from the repository
            var user = await _repository.GetByIdAsync(query.UserId);
            if (user != null)
            {
                // Cache the user for future use (set the expiration as needed)
                await _cache.SetAsync(cacheKey, user, TimeSpan.FromMinutes(10));  // Set cache with 10-minute expiration
            }

            return user;  // Return the user (either from cache or the repository)
        }
    }
}
