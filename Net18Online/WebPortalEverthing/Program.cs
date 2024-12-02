using Everything.Data;
using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;
using Everything.Data.Repositories;
using Everything.Data.Repositories.Surveys;
using MazeCore.Builders;
using Microsoft.EntityFrameworkCore;
using SimulatorOfPrinting.Models;
using WebPortalEverthing.Services;
using AnimeGirlRepository = Everything.Data.Repositories.AnimeGirlRepository;
using CoffeShopRepository = Everything.Data.Repositories.CoffeShopRepository;
using EcologyRepository = Everything.Data.Repositories.EcologyRepository;
using GameStoreRepository = Everything.Data.Repositories.GameStoreRepository;
using LoadTestingRepository = Everything.Data.Repositories.LoadTestingRepository;
using AnimeCatalogRepository = Everything.Data.Repositories.AnimeCatalogRepository;
using TypeOfApplianceRepository = Everything.Data.Repositories.TypeOfApplianceRepository;
using WebPortalEverthing.Services.LoadTesting;
using WebPortalEverthing.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(AuthService.AUTH_TYPE_KEY)
    .AddCookie(AuthService.AUTH_TYPE_KEY, config =>
    {
        config.LoginPath = "/Auth/Login";
        config.AccessDeniedPath = "/Home/Forbidden";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebDbContext>(x => x.UseSqlServer(WebDbContext.CONNECTION_STRING));
//builder.Services.AddDbContext<WebDbContext>(options => options.UseNpgsql(WebDbContext.CONNECTION_STRING));


// Register in DI container services/repository for ServiceCenter
builder.Services.AddScoped<ITypeOfApplianceRepositoryReal, TypeOfApplianceRepository>();

builder.Services.AddSingleton<IDNDRepository, DNDRepository>();
builder.Services.AddSingleton<IChessPartiesRepository, ChessPartiesRepository>();

builder.Services.AddScoped<IMoviePosterRepositoryReal, MoviePosterRepository>();

builder.Services.AddScoped<IAnimeCatalogRepositoryReal, AnimeCatalogRepository>();
builder.Services.AddScoped<IAnimeReviewRepositoryReal, AnimeReviewsRepository>();
builder.Services.AddScoped<IEcologyRepositoryReal, EcologyRepository>();
builder.Services.AddScoped<ICommentRepositoryReal, CommentRepository>();
builder.Services.AddScoped<IKeyCoffeShopRepository, CoffeShopRepository>();
builder.Services.AddScoped<IMangaRepositoryReal, MangaRepository>();
builder.Services.AddScoped<IBrandRepositoryReal, BrandRepository>();
builder.Services.AddScoped<IFilmDirectorRepositoryReal, FilmDirectorRepository>();
builder.Services.AddScoped<IAnimeGirlRepositoryReal, AnimeGirlRepository>();
builder.Services.AddScoped<ISurveyGroupRepositoryReal, SurveyGroupRepository>();
builder.Services.AddScoped<IStatusRepositoryReal, StatusRepository>();
builder.Services.AddScoped<ISurveysRepositoryReal, SurveysRepository>();
builder.Services.AddScoped<IDocumentRepositoryReal, DocumentRepository>();
builder.Services.AddScoped<IGameStoreRepositoryReal, GameStoreRepository>();
builder.Services.AddScoped<IGameStudiosRepositoryReal, GameStudiosRepository>();
builder.Services.AddScoped<ICakeRepositoryReal, CakeRepository>();
builder.Services.AddScoped<IMagazinRepositoryReal, MagazinRepository>();

builder.Services.AddScoped<ILoadTestingRepositoryReal, LoadTestingRepository>();
builder.Services.AddScoped<ILoadVolumeTestingRepositoryReal, LoadVolumeTestingRepository>();
builder.Services.AddScoped<ILoadUserRepositryReal, LoadUserRepository>();
builder.Services.AddScoped<IUserRepositryReal, UserRepository>();

builder.Services.AddScoped<HelperForValidatingCake>();
builder.Services.AddScoped<TextProvider>();
builder.Services.AddScoped<MazeBuilder>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LoadAuthService>();
builder.Services.AddScoped<EnumHelper>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FileProvider>();
builder.Services.AddScoped<HelperForFile>();

builder.Services.AddSingleton<IGameLifeRepository, GameLifeRepository>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
// Load data into repository from JSON file
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var typeOfApplianceRepo = scope.ServiceProvider.GetRequiredService<ITypeOfApplianceRepositoryReal>();
    var jsonFilePath = Path.Combine(app.Environment.ContentRootPath, "Data", "ServiceCenter", "typeOfAppliance.json");
    typeOfApplianceRepo.LoadDataFromJson(jsonFilePath);
}

var seed = new Seed();
seed.Fill(app.Services);

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

app.UseAuthentication(); // Who Am I?
app.UseAuthorization(); // May I?

app.UseMiddleware<CustomLocalizationMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
