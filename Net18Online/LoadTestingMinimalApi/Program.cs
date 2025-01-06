using LoadTestingMinimalApi.DBstuff;
using LoadTestingMinimalApi.DBstuff.DataModels;
using LoadTestingMinimalApi.Hubs;
using LoadTestingMinimalApi.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();// собирает инфу про все endpoints
builder.Services.AddSwaggerGen(); //swagger генерирует json
                                  //

// Добавляем DbContext с конфигурацией из appsettings.json
builder.Services.AddDbContext<ChatDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ChatDb");
    options.UseSqlServer(connectionString);
    // options.UseNpgsql(connectionString);
});

// Другие настройки приложения
var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.MapGet("/getMetrics", (ChatDbContext chatDbContext) =>
{
    return chatDbContext.Metrics.Select(dataModel => new MetricViewModel
    {
        Id = dataModel.Id,
        Average = dataModel.Average,
        Throughput = dataModel.Throughput,
        Name = dataModel.Name,
        CreatorName = dataModel.CreatorName,
        LoadVolumeName = dataModel.LoadVolumeName,
        CanDelete = dataModel.CanDelete,
        IsLiked = dataModel.IsLiked,
    }).ToList();
});


app.MapPost("/addMetric", (ChatDbContext chatDbContext, [FromBody] MetricViewModel model) =>
{
    //viewmodel перевести в dataModel, и dataModel записать в БД
    MetricData metricData = new MetricData()
    {
        Average = model.Average,
        Throughput = model.Throughput,
        Name = model.Name,
        CreatorName = model.CreatorName,
        LoadVolumeName = model.LoadVolumeName,
        CanDelete = model.CanDelete,
        IsLiked = model.IsLiked,
    };

    chatDbContext.Metrics.Add(metricData);
    chatDbContext.SaveChanges();

    return $"""
    Metric added:
        Id: {model.Id}
        Guid: {model.Guid}
        Name: {model.Name}
        Throughput: {model.Throughput}
        Average: {model.Average}
        CreatorName: {model.CreatorName}
        LoadVolumeName: {model.LoadVolumeName}
        CanDelete: {model.CanDelete}
        IsLiked: {model.IsLiked}
        LikeCount: {model.LikeCount}
    """;
});




app.UseSwagger();
/*  красивая UI для swagger , чтобы в адрес добавить /swagger  наподобие, 
   https://localhost:7121/swagger/index.html   */
app.UseSwaggerUI();



app.MapHub<LoadChatHub>("/loadChatMini");



app.Run();
