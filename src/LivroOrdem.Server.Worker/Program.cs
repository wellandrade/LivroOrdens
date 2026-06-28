using LivroOrdem.Server.Worker;
using LivroOrdens.Aplicacao.DI;
using LivroOrdens.Fix.DI;
using LivroOrdens.Infra.DI;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddServerApplication();
builder.Services.AddInfra();
builder.Services.AddFixServer();

var host = builder.Build();
host.Run();
