using ShoppingCart.API.Endpoints;
using ShoppingCart.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.AddDbContext();
builder.AddRepositories();
builder.AddUseCases();
builder.AddMessageBusConfiguration();
builder.AddJwtConfiguration();
builder.AddDocumentationConfig();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseSecurity();

app.MapEndpoints();
app.Run();