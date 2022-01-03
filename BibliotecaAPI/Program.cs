using BibliotecaAPI.Endpoints;
using BibliotecaAPI.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.AddPersistence();
var app = builder.Build();

app.MapLivrosEndpoints();
app.MapAutoresEndpoints();
app.MapProfessoresEndPoints();
app.MapAlunosEndpoints();
app.MapEmprestimosDeAlunosEndPoints();
app.MapEmprestimosDeProfessoresEndpoints();

//app.MapGet("/", () => "Hello World!");

app.Run();
