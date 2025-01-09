using LoadTestingMinimalApi.DBstuff;
using LoadTestingMinimalApi.DBstuff.DataModels;
using LoadTestingMinimalApi.Hubs;
using LoadTestingMinimalApi.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

///����� ��������� Swagger ��� ������ API � �������� �������� �������, � ����� ������� ��������� ������, ��� ����� ��������������� ����������� �� ������ Swashbuckle.AspNetCore
///�������� ��������� ��� �������: ����������� �������� [ProducesResponseType] � [Produces] ��� �������� ������������ ����� � ��������� ������.
///    �������� Swagger:
///��������� �������� API ����� SwaggerDoc.
///
///��������� ���������:
///������������.Produces, .WithTags, .WithName ��� ���������������� API.
///������� ���� �������� (200, 400, 500) � ���� ������������ ������.
///��������� ������:
///        ����������� Results.Problem ��� ����������� ��������� �� �������.
///���������� ���������� Swagger UI:
///������ ������������� � ��������� Metrics � Messages.
///������ Swagger ����� �������� �� ������ https://localhost:7121/index.html ���� https://localhost:7121/swagger

// Swagger � ��� ���������:

builder.Services.AddEndpointsApiExplorer();// �������� ���� ��� ��� endpoints
//swagger ���������� json
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Load Testing Minimal API",
        Version = "v1",
        Description = "API ��� ������ � ����� � ���������."
    });
});

// SignalR � �������� ���� ������
builder.Services.AddSignalR();//��� ���� � ����
builder.Services.AddDbContext<ChatDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ChatDb");
    options.UseSqlServer(connectionString);
    // options.UseNpgsql(connectionString);
});

// ��������� CORS
//��������� ������� ��������� ������ � ������ ������
builder.Services.AddCors(option =>
{
   option.AddDefaultPolicy(policy =>
   {
       policy.AllowAnyHeader();
       policy.AllowAnyMethod();
       //� ����� ����� �������� ��������� ������ (��� �� ����)
       policy.SetIsOriginAllowed(origin => true);//��� ����� �������� ����� ������ ��������
                                                 // ������� ����� � � ���������������, � ��� ������������, �����
                                                 //����� ��������� �������
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
              .WithOrigins("https://localhost:7121"); // ������� ���������� URL
    });
});
*/

// ������ ��������� ����������
var app = builder.Build();

app.UseSwagger();
/*  �������� UI ��� swagger , ����� � ����� �������� /swagger  ���������, https://localhost:7121/index.html
   https://localhost:7121/swagger/index.html   */
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Load Testing Minimal API v1");
    c.RoutePrefix = string.Empty; // Swagger �������� �� ��������� ������
});

//������������� ���������� ����
app.UseCors();

// �������� ��� ��������� ������
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

// �������� ��� ���������� ������
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

// �������� ��� ��������� ���������
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

// �������� ��� ���������� ���������
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
// ����� ��� � ������� WebPortalEverthing, ����� �� ������� ����� miniApi


app.Run();
