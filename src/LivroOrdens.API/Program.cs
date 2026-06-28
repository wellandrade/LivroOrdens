using LivroOrdens.API.Middleware;
using LivroOrdens.Aplicacao.DI;
using LivroOrdens.Fix.DI;
using LivroOrdens.Infra.DI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
//builder.Services.AddFixServer();
builder.Services.AddApplication();
builder.Services.AddInfra();
builder.Services.AddFixClient();

var app = builder.Build();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    
}

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
