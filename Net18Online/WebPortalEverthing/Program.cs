using Everything.Data;
using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;
using Everything.Data.Repositories;
using MazeCore.Builders;
using Microsoft.EntityFrameworkCore;
using SimulatorOfPrinting.Models;
using AnimeGirlRepository = Everything.Data.Repositories.AnimeGirlRepository;
using CoffeShopRepository = Everything.Data.Repositories.CoffeShopRepository;
using EcologyRepository = Everything.Data.Repositories.EcologyRepository;
using TypeOfApplianceRepository = Everything.Data.Repositories.TypeOfApplianceRepository;
using Everything.Data.Models;
using Everything.Data.Repositories.Surveys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebDbContext>(x => x.UseSqlServer(WebDbContext.CONNECTION_STRING));
//builder.Services.AddDbContext<WebDbContext>(options => options.UseNpgsql(WebDbContext.CONNECTION_STRING));


// Register in DI container our services/repository

// Register in DI container services/repository for ServiceCenter
builder.Services.AddScoped<ITypeOfApplianceRepositoryReal, TypeOfApplianceRepository>();

builder.Services.AddSingleton<IGameStoreRepository, GameStoreRepository>();
builder.Services.AddSingleton<IDNDRepository, DNDRepository>();

builder.Services.AddScoped<IMoviePosterRepositoryReal, MoviePosterRepository>();

builder.Services.AddScoped<IAnimeCatalogRepository, AnimeCatalogRepository>();
builder.Services.AddScoped<IEcologyRepositoryReal, EcologyRepository>();
builder.Services.AddScoped<IKeyCoffeShopRepository, CoffeShopRepository>();
builder.Services.AddScoped<IMangaRepositoryReal, MangaRepository>();
builder.Services.AddScoped<IBrandRepositoryReal, BrandRepository>();

builder.Services.AddScoped<IAnimeGirlRepositoryReal, AnimeGirlRepository>();
builder.Services.AddScoped<ISurveyGroupRepositoryReal, SurveyGroupRepository>();
builder.Services.AddScoped<IStatusRepositoryReal, StatusRepository>();
builder.Services.AddScoped<ISurveysRepositoryReal, SurveysRepository>();

builder.Services.AddScoped<ICakeRepositoryReal, CakeRepository>();

builder.Services.AddScoped<TextProvider>();
builder.Services.AddScoped<MazeBuilder>();

builder.Services.AddSingleton<ILoadTestingRepository, LoadTestingRepository>();
builder.Services.AddSingleton<IGameLifeRepository, GameLifeRepository>();

var app = builder.Build();
// Load data into repository from JSON file
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var typeOfApplianceRepo = scope.ServiceProvider.GetRequiredService<ITypeOfApplianceRepositoryReal>();
    var jsonFilePath = Path.Combine(app.Environment.ContentRootPath, "Data", "ServiceCenter", "typeOfAppliance.json");
    typeOfApplianceRepo.LoadDataFromJson(jsonFilePath);
}

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
