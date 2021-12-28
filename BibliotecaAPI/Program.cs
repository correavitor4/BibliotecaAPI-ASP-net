using BibliotecaAPI.Endpoints;
using BibliotecaAPI.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.AddPersistence();
var app = builder.Build();

app.MapLivrosEndpoints();

//app.MapGet("/", () => "Hello World!");

app.Run();
