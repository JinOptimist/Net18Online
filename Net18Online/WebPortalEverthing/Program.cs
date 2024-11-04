using Everything.Data;
using Everything.Data.Repositories;
using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;
using MazeCore.Builders;
using SimulatorOfPrinting.Models;
using Microsoft.EntityFrameworkCore;
using AnimeGirlRepository = Everything.Data.Repositories.AnimeGirlRepository;
using Everything.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebDbContext>(x => x.UseSqlServer(WebDbContext.CONNECTION_STRING));

// Register in DI container our services/repository
builder.Services.AddSingleton<IEcologyRepository, EcologyRepository>();
builder.Services.AddSingleton<ICoffeShopRepository, CoffeShopRepository>();
builder.Services.AddSingleton<ISurveyGroupRepository, SurveyGroupRepository>();
builder.Services.AddSingleton<IStatusRepository, StatusRepository>();
builder.Services.AddSingleton<ISurveysRepository, SurveysRepository>();
// Register in DI container services/repository for ServiceCenter
builder.Services.AddSingleton<ITypeOfApplianceRepository, TypeOfApplianceRepository>();

builder.Services.AddSingleton<IGameStoreRepository, GameStoreRepository>();
builder.Services.AddSingleton<IDNDRepository, DNDRepository>();

// Register in DI container services/repository for MoviePosterRepository
builder.Services.AddSingleton<IMoviePosterRepository, MoviePosterRepository>();

builder.Services.AddScoped<IAnimeCatalogRepository, AnimeCatalogRepository>();

builder.Services.AddScoped<IAnimeGirlRepositoryReal, AnimeGirlRepository>();

builder.Services.AddScoped<ICakeRepositoryReal, CakeRepository>();

builder.Services.AddScoped<TextProvider>();
builder.Services.AddScoped<MazeBuilder>();

builder.Services.AddSingleton<ILoadTestingRepository, LoadTestingRepository>();
builder.Services.AddSingleton<IGameLifeRepository, GameLifeRepository>();

var app = builder.Build();
// Load data into repository from JSON file
var typeOfApplianceRepo = app.Services.GetRequiredService<ITypeOfApplianceRepository>();
var jsonFilePath = Path.Combine(app.Environment.ContentRootPath, "Data", "ServiceCenter", "typeOfAppliance.json");
typeOfApplianceRepo.LoadDataFromJson(jsonFilePath);

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
