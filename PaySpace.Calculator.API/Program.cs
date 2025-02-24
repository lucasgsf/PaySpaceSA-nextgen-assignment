using Mapster;
using PaySpace.Calculator.API.Extensions;
using PaySpace.Calculator.API.Middlewares;
using PaySpace.Calculator.Borders.Extensions;
using PaySpace.Calculator.Data.Extensions;
using PaySpace.Calculator.Services.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMapster();
builder.Services.AddExtensions(Assembly.GetExecutingAssembly());

builder.Services.AddConverters();
builder.Services.AddServices();
builder.Services.AddDataServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.InitializeDatabase();

app.Run();