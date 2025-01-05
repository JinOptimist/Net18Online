var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();// �������� ���� ��� ��� endpoints
builder.Services.AddSwaggerGen(); //swagger ���������� json                                          

var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.UseSwagger();
/*  �������� UI ��� swagger , ����� � ����� �������� /swagger  ���������, 
   https://localhost:7121/swagger/index.html   */
app.UseSwaggerUI();






app.Run();
