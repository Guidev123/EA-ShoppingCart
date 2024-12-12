using ShoppingCart.API.Endpoints;
using ShoppingCart.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddDbContext();
builder.AddRepositories();
builder.AddUseCases();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();