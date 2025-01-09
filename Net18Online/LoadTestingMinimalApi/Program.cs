using LoadTestingMinimalApi.DBstuff;
using LoadTestingMinimalApi.DBstuff.DataModels;
using LoadTestingMinimalApi.Hubs;
using LoadTestingMinimalApi.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

///Чтобы настроить Swagger для вашего API и добавить описания методов, а также указать возможные ошибки, вам нужно воспользоваться аннотациями из пакета Swashbuckle.AspNetCore
///Добавьте аннотации для методов: Используйте атрибуты [ProducesResponseType] и [Produces] для указания возвращаемых типов и возможных ошибок.
///    Настроен Swagger:
///Добавлено описание API через SwaggerDoc.
///
///Добавлены аннотации:
///Использованы.Produces, .WithTags, .WithName для документирования API.
///Указаны коды статусов (200, 400, 500) и типы возвращаемых данных.
///Обработка ошибок:
///        Использован Results.Problem для возвращения сообщений об ошибках.
///Улучшенная читаемость Swagger UI:
///Методы сгруппированы в категории Metrics и Messages.
///Теперь Swagger будет доступен по адресу https://localhost:7121/index.html было https://localhost:7121/swagger

// Swagger и его аннотации:

builder.Services.AddEndpointsApiExplorer();// собирает инфу про все endpoints
//swagger генерирует json
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Load Testing Minimal API",
        Version = "v1",
        Description = "API для работы с чатом и метриками."
    });
});

// SignalR и контекст базы данных
builder.Services.AddSignalR();//для хаба и чата
builder.Services.AddDbContext<ChatDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ChatDb");
    options.UseSqlServer(connectionString);
    // options.UseNpgsql(connectionString);
});

// Настройка CORS
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

/*
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("https://localhost:7121"); // Укажите клиентский URL
    });
});
*/

// Другие настройки приложения
var app = builder.Build();

app.UseSwagger();
/*  красивая UI для swagger , чтобы в адрес добавить /swagger  наподобие, https://localhost:7121/index.html
   https://localhost:7121/swagger/index.html   */
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Load Testing Minimal API v1");
    c.RoutePrefix = string.Empty; // Swagger доступен по корневому адресу
});

//Использование разрешений выше
app.UseCors();

// Эндпоинт для получения метрик
app.MapGet("/getMetrics",
    ([FromServices] ChatDbContext chatDbContext) =>
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
    })
    .WithTags("Metrics")
    .WithName("GetMetrics")
    .Produces<List<MetricViewModel>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status500InternalServerError);

// Эндпоинт для добавления метрик
app.MapPost("/addMetric",
    ([FromServices] ChatDbContext chatDbContext, [FromBody] MetricViewModel model) =>
    {
        try
        {
            var metricData = new MetricData
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

            return Results.Ok(new { Message = "Metric successfully added.", MetricId = metricData.Id });
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message, statusCode: 500);
        }
    })
    .WithTags("Metrics")
    .WithName("AddMetric")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);

// Эндпоинт для получения сообщений
app.MapGet("/getMessage",
    ([FromServices] ChatDbContext chatDbContext) =>
    {
        return chatDbContext.Messages.Select(message => new ChatMessageViewModel
        {
            Id = message.Id,
            AuthorId = message.AuthorId,
            AuthorName = message.AuthorName,
            Text = message.Text,
        }).ToList();
    })
    .WithTags("Messages")
    .WithName("GetMessages")
    .Produces<List<ChatMessageViewModel>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status500InternalServerError);

// Эндпоинт для добавления сообщений
app.MapPost("/addMessage",
    ([FromServices] ChatDbContext chatDbContext, [FromBody] AddMessageViewModel viewModel) =>
    {
        try
        {
            var message = new Message
            {
                AuthorId = viewModel.AuthorId,
                AuthorName = viewModel.AuthorName,
                Text = viewModel.Text,
            };

            chatDbContext.Add(message);
            chatDbContext.SaveChanges();

            return Results.Ok(new { Message = "Message successfully added.", MessageId = message.Id });
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message, statusCode: 500);
        }
    })
    .WithTags("Messages")
    .WithName("AddMessage")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);


app.MapHub<LoadChatHub>("/hub/loadChatmini");
// адрес как в проекте WebPortalEverthing, чтобы он работал через miniApi


app.Run();
