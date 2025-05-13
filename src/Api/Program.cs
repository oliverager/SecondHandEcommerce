using Application.Handlers.CommandHandlers;
using Application.Handlers.QueryHandlers;
using Application.Interfaces;
using Infrastructure.Cache;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Repositories;
using Infrastructure.Services;
using StackExchange.Redis;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoSettings")
        .Get<MongoSettings>();
    return new MongoClient(settings!.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoSettings")
        .Get<MongoSettings>();
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings!.DatabaseName);
});

builder.Services.AddScoped<MongoContext>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// CommandHandler
builder.Services.AddScoped<CreateReviewCommandHandler>();
builder.Services.AddScoped<CreateListingCommandHandler>();
builder.Services.AddScoped<CreateOrderCommandHandler>();
builder.Services.AddScoped<CancelOrderCommandHandler>();
builder.Services.AddScoped<CreateUserCommandHandler>();

// QueryHandler
builder.Services.AddScoped<GetAllListingsQueryHandler>();
builder.Services.AddScoped<GetListingByIdQueryHandler>();
builder.Services.AddScoped<GetOrderByIdQueryHandler>();
builder.Services.AddScoped<GetAllOrdersQueryHandler>();
builder.Services.AddScoped<GetUserByIdQueryHandler>();
builder.Services.AddScoped<GetReviewByIdQueryHandler>();
builder.Services.AddScoped<GetReviewsBySellerIdQueryHandler>();

// Cache abstraction
builder.Services.AddSingleton<ICacheService, RedisCacheService>();


builder.Services.AddScoped(sp =>
{
    var db = sp.GetRequiredService<IMongoDatabase>();
    var client = sp.GetRequiredService<IMongoClient>();
    return new MongoContext(db, client);
});

// Redis configuration (from appsettings or hardcoded)
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis")
                        ?? "localhost:6379"; // fallback default
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SecondHand E-Commerce API",
        Version = "v1",
        Description = "Backend API for a second-hand item marketplace built with .NET and MongoDB.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Oliver Ager Jørgensen & Alexander Moretto Stengaard",
            Url = new Uri("https://github.com/oliverager/SecondHandEcommerce")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});


// Other DI (repos, handlers, etc.)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    // Triggers indexes.
    var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
    var listingRepo = scope.ServiceProvider.GetRequiredService<IListingRepository>();
    var orderRepo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
    var reviewRepo = scope.ServiceProvider.GetRequiredService<IReviewRepository>();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();