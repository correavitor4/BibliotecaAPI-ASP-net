using BibliotecaAPI.Data;
using Dapper.Contrib.Extensions;
using static BibliotecaAPI.Data.BancoContext;


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
                    return Results.NotFound("Nenhum livro foi encontrado");
                }
                
                return Results.Ok(livros);
            });

            app.MapGet("/livros/{id}", async (GetConnection connectionGetter,int id) =>
            {
                using var con = await connectionGetter();
                
                var livros = con.Get<Livros>(id);
                if (livros is null)
                {
                    return Results.NotFound("Não foi encontrado nenhum livro com esse Id");
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
                    return Results.BadRequest("Não foi passado nenhum livro ou o livro foi passado incorretamente");
                }

                var idAutor = livro.Autor;

                var autor = con.Get<Autores>(idAutor);
                if(autor is null)
                {
                    return Results.BadRequest("O id do autor não é válido");
                }
                
                try
                {
                    var id = con.Insert<Livros>(livro);
                    return Results.Created($"/livros/{id}", livro);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(statusCode:500);
                }
                

            });

            app.MapPut("/livros", async (GetConnection connectionGetter, Livros livro) =>
             {
                 using var con = await connectionGetter();
                 if(livro is null)
                 {

                     return Results.BadRequest("Não foi fornecido nenhum livro");
                 }
                 try
                 {
                     con.Update<Livros>(livro);
                     return Results.Ok();
                 }
                 catch (Exception ex)
                 {
                     System.Diagnostics.Debug.WriteLine(ex.ToString());
                     return Results.StatusCode(statusCode:500);
                 }
             });

            app.MapDelete("/livros/{id}", async(GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                /*if(id is null)
                {
                    return Results.BadRequest("Não foi fornecido nenhum id");
                }*/
                var deleted = con.Get<Livros>(id);
                
                if (deleted is null)
                {
                    return Results.NotFound("Não foi encontrado nenhum registro de livro com esse Id");
                }

                try
                {
                    
                    con.Delete(deleted);
                    return Results.Ok();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.Ok(deleted);
                }
            });
        }
    }
}
