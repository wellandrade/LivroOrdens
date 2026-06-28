using LivroOrdens.Aplicacao.DI;
using LivroOrdens.Client.Worker;
using LivroOrdens.Fix.DI;
using LivroOrdens.Infra.DI;

var builder = WebApplication.
builder.Services.AddHostedService<Worker>();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfra();
builder.Services.AddFixServer();



var host = builder.Build();
host.Run();
