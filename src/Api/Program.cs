using Application.Handlers.CommandHandlers;
using Application.Handlers.QueryHandlers;
using Application.Interfaces;
using Infrastructure.Cache;
using Infrastructure.Mongo;
using Infrastructure.Mongo.Repositories;
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

// CommandHandler
builder.Services.AddScoped<CreateListingCommandHandler>();
builder.Services.AddScoped<CreateOrderCommandHandler>();

// QueryHandler
builder.Services.AddScoped<GetAllListingsQueryHandler>();
builder.Services.AddScoped<GetListingByIdQueryHandler>();
builder.Services.AddScoped<GetOrderByIdQueryHandler>();
builder.Services.AddScoped<GetAllOrdersQueryHandler>();




// Redis configuration (from appsettings or hardcoded)
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis") 
                        ?? "localhost:6379"; // fallback default
    return ConnectionMultiplexer.Connect(configuration);
});

// Cache abstraction
builder.Services.AddSingleton<ICacheService, RedisCacheService>();

// Other DI (repos, handlers, etc.)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
