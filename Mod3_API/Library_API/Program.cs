global using System.ComponentModel.DataAnnotations; 
global using Library_API.Models;
global using Library_API.Data.Repository;
global using System.Data.SqlClient;
global using System.Data;

using Library_API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<BooksAction, BooksAction>();
builder.Services.AddScoped<BooksRepository, BooksRepository>();

builder.Services.AddScoped<NucleosAction, NucleosAction>();
builder.Services.AddScoped<NucleosRepository, NucleosRepository>();

builder.Services.AddScoped<StockAction, StockAction>();
builder.Services.AddScoped<StockRepository, StockRepository>();

builder.Services.AddScoped<PendingEntregaAction, PendingEntregaAction>();
builder.Services.AddScoped<PendingEntregaRepository, PendingEntregaRepository>();

builder.Services.AddScoped<ReaderAction, ReaderAction>();
builder.Services.AddScoped<ReaderRepository, ReaderRepository>();

builder.Services.AddScoped<TransactionsAction, TransactionsAction>();
builder.Services.AddScoped<TransactionsRepository, TransactionsRepository>();

builder.Services.AddScoped<StatsAction, StatsAction>();
builder.Services.AddScoped<StatsRepository, StatsRepository>();

builder.Services.AddScoped<ImagensAction, ImagensAction>();
builder.Services.AddScoped<ImagensRepository, ImagensRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
