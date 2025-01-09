using LoadTestingMinimalApi.DBstuff;
using LoadTestingMinimalApi.DBstuff.DataModels;
using LoadTestingMinimalApi.Hubs;
using LoadTestingMinimalApi.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();// собирает инфу про все endpoints
builder.Services.AddSwaggerGen(); //swagger генерирует json

builder.Services.AddSignalR(); //дл€ хаба и чата


// ƒобавл€ем DbContext с конфигурацией из appsettings.json
builder.Services.AddDbContext<ChatDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ChatDb");
    options.UseSqlServer(connectionString);
    // options.UseNpgsql(connectionString);
});

//разрешить серверу принимать данные с других сайтов
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        //с каких типов серверов принимаем данные (тут со всех)
        policy.SetIsOriginAllowed(origin => true);//тут можно добавить белый список серверов
        // запросы можно и с аутентификацией, и без аунтификации, любые
        //можно настроить правила
        policy.AllowCredentials();
    });
});

/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("https://localhost:7121"); // ”кажите ваш клиентский URL
    });
});*/




// ƒругие настройки приложени€
var app = builder.Build();


//app.UseCors();


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
    //viewmodel перевести в dataModel, и dataModel записать в Ѕƒ
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


app.MapGet("/getMessage", (ChatDbContext chatDbContext) =>
{
    return chatDbContext.Messages.Select(message => new ChatMessageViewModel
    {
        Id = message.Id,
        AuthorId = message.AuthorId,
        AuthorName = message.AuthorName,
        Text = message.Text,
    }).ToList();
});

app.MapPost("/addMessage", (ChatDbContext chatDbContext, [FromBody] AddMessageViewModel viewModel) =>
    {
        var message = new Message
        {
            AuthorId = viewModel.AuthorId,
            AuthorName = viewModel.AuthorName,
            Text = viewModel.Text,
        };
        chatDbContext.Add(message);
        chatDbContext.SaveChanges();
        return $"Message Id: {message.Id} added";
    });




app.UseSwagger();
/*  красива€ UI дл€ swagger , чтобы в адрес добавить /swagger  наподобие, 
   https://localhost:7121/swagger/index.html   */
app.UseSwaggerUI();


app.UseCors();

app.MapHub<LoadChatHub>("/hub/loadChatmini");
// адрес как в проекте WebPortalEverthing, чтобы он работал через miniApi 



app.Run();
