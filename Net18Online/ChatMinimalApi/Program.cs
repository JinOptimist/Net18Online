using ChatMinimalApi.DbStuff;
using ChatMinimalApi.DbStuff.Models;
using ChatMinimalApi.Hubs;
using ChatMinimalApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddDbContext<ChatDbContext>(o => o.UseSqlServer(ChatDbContext.CONNECTION_STRING));

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.SetIsOriginAllowed(origin => true);
        policy.AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");

app.MapGet("/smile", () => "Happy");

app.MapGet("/calc", (int x, int y) =>
{
    var answer = x + y;
    return answer;
});

app.MapGet("/getMessages", (ChatDbContext dbContext) =>
{
    return dbContext.Messages.Select(x => new ChatMessageViewModel
    {
        Id = x.Id,
        Text = x.Text,
        AuthorId = x.AuthorId,
        AuthorName = x.AuthorName,
    }).ToList();
});

app.MapPost("/addMessage", (ChatDbContext dbContext, [FromBody] AddMessageViewModel vm) =>
{
    var message = new Message
    {
        AuthorId = vm.AuthorId,
        AuthorName = vm.AuthorName,
        Text = vm.Text
    };
    dbContext.Messages.Add(message);
    dbContext.SaveChanges();

    return $"Message is added. Id: {message.Id}";
});

app.UseSwagger();
app.UseSwaggerUI();

app.MapHub<ChatHub>("/hub/chat");

app.Run();
