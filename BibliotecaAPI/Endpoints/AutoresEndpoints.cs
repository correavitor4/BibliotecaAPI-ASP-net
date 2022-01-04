
using BibliotecaAPI.Data;
using Dapper.Contrib.Extensions;
using static BibliotecaAPI.Data.BancoContext;

namespace BibliotecaAPI.Endpoints
{
    public static class AutoresEndpoints
    {
        public static void MapAutoresEndpoints(this WebApplication app)
        {
            app.MapGet("/autores", async (GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                List<Autores> autores = new List<Autores>();
                try
                {
                    autores = con.GetAll<Autores>().ToList();
                    
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return Results.StatusCode(500);
                }
                
                if(autores is null)
                {
                    return Results.NotFound("Não foi encontrado nenhum autor");

                }
                return Results.Ok(autores);
            });

            app.MapGet("/autores/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                Autores autor;
                try
                {
                    autor = con.Get<Autores>(id);
                    if(autor is null)
                    {
                        return Results.NotFound("Não foi encontrado nenhum autor correspondente ao Id fornecido na URL");
                    }
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
                return Results.Ok(autor);
            });

            app.MapPost("/autores", async(GetConnection connectionGetter,Autores autor) =>
            {
                using var con = await connectionGetter();
                if(autor is null)
                {
                    return Results.BadRequest("Não foi fornecido nenhum autor");
                }
                try
                {
                    var id = con.Insert<Autores>(autor);
                    return Results.Created($"/autores/{id}",autor);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapPut("/autores", async (GetConnection connectionGetter, Autores autor) =>
            {
                using var con = await connectionGetter();
                if(autor is null)
                {
                    return Results.BadRequest("Não  foi fornecido nenhum autor");
                }

                try
                {
                    con.Update<Autores>(autor);
                    
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
                return Results.Ok();
            });

            app.MapDelete("/autores/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                var deleted = con.Get<Autores>(id);

                if(deleted is null)
                {
                    return Results.NotFound("Não foi encotrado nenhum autor correspondente ao Id fornecido na requisição");
                }


                try
                {
                    con.Delete<Autores>(deleted);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return Results.StatusCode(500);
                }

                
                return Results.Ok();
                
            });
        }
    }
}
