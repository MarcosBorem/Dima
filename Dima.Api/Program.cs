using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(cnnStr);
    });
    
//ADICIONA SUPORTE PARA O OPEN API
builder.Services.AddEndpointsApiExplorer();

//ADICIONA INTERFACE DE DOCUMENTA��O DO SWAGGER
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
// Nova inst�ncia para cada requisi��o (transient) e o descarta ap�s usar
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
var app = builder.Build();

// Utiliza o swagger
app.UseSwagger();
//Gera a tela
app.UseSwaggerUI();

app.MapEndpoints();
app.Run();
