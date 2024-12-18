using Everything.Data.Repositories;
using Everything.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IKeyCoffeShopRepository, CoffeShopRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/getCoffeByCreator/{creatorId}", (IKeyCoffeShopRepository repository, int creatorId) =>
{
    var result = repository.GetAllByCreatorId(creatorId);
    return Results.Ok(result);
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();