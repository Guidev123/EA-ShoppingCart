using ShoppingCart.API.Endpoints;
using ShoppingCart.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.AddDbContext();
builder.AddRepositories();
builder.AddUseCases();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();