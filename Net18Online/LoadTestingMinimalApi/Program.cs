var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();// собирает инфу про все endpoints
builder.Services.AddSwaggerGen(); //swagger генерирует json                                          

var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.UseSwagger();
/*  красивая UI для swagger , чтобы в адрес добавить /swagger  наподобие, 
   https://localhost:7121/swagger/index.html   */
app.UseSwaggerUI();






app.Run();
