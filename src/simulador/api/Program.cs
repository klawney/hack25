using api.Endpoints;
using api.Middleware;
using Microsoft.AspNetCore.Rewrite;
using api.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddEventHubProducer(builder.Configuration);
//builder.Services.AddEventHubProducer(builder.Configuration);
//builder.Services.AddIoC();

builder.Services.AddMssqlDapper(builder.Configuration);

var app = builder.Build();

// Adiciona o middleware de telemetria
app.UseMiddleware<TelemetriaMiddleware>();
app.UseMiddleware<TelemetriaMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //redireciona para o swwagger qdue acessar a raiz
    app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger/index.html"));
}
app.MapEndpointSimulacao();

app.MapEndpointTelemetria();

app.UseHttpsRedirection();


app.Run();