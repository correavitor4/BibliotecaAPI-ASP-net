using BibliotecaAPI.Data;
using Dapper.Contrib.Extensions;
using static BibliotecaAPI.Data.LivrosContext;

namespace BibliotecaAPI.Endpoints
{
    public static class LivrosEndpoints
    {
        public static void MapLivrosEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => "Raíz endpoint");
            app.MapGet("/livros", async (GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                var livros = con.GetAll<Livros>().ToList();
                if(livros is null)
                {
                    return Results.NotFound();
                }
                
                return Results.Ok(livros);
            });

            //O connectionGetter deve ser uma variável que busca a conexão
            app.MapPost("/livros", async (GetConnection connectionGetter, Livros livro) =>
            {
             
                //acho que é a variável que armazena a conexão
                using var con = await connectionGetter();
                if (livro is null)
                {
                    return Results.BadRequest();
                }
                
                try
                {
                    var id = con.Insert(livro);
                    return Results.Created($"/livros/{id}", livro);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(statusCode:500);
                }
                

            });
        }
    }
}
