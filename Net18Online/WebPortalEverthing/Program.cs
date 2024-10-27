using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;
using MazeCore.Builders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register in DI container our services/repository
builder.Services.AddSingleton<IAnimeGirlRepository, AnimeGirlRepository>();
builder.Services.AddSingleton<ICakeRepository, CakeRepository>();
builder.Services.AddSingleton<IEcologyRepository, EcologyRepository>();
builder.Services.AddSingleton<ICoffeShopRepository, CoffeShopRepository>();
builder.Services.AddSingleton<ISurveysRepository, SurveysRepository>();
builder.Services.AddSingleton<IGameStoreRepository, GameStoreRepository>();


// Register in DI container services/repository for MoviePosterRepository
builder.Services.AddSingleton<IMoviePosterRepository, MoviePosterRepository>();


builder.Services.AddScoped<IAnimeCatalogRepository, AnimeCatalogRepository>();

builder.Services.AddScoped<MazeBuilder>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
