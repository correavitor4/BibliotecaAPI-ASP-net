using BibliotecaAPI.Data;
using Dapper.Contrib.Extensions;
using static BibliotecaAPI.Data.BancoContext;

namespace BibliotecaAPI.Endpoints
{
    public static class ProfessoresEndpoints
    {
        public static void MapProfessoresEndPoints(this WebApplication app)
        {
            app.MapGet("/professores", async(GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                List<Professores> professores = new List<Professores>();
                try { 
                    professores = con.GetAll<Professores>().ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }

                if(professores is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(professores);
            });

            app.MapGet("/professores/{id}", async(GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                Professores professor;
                try
                {
                    professor = con.Get<Professores>(id);    
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }

                if(professor is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(professor);
            });

            app.MapPost("/professores", async(GetConnection connectionGetter, Professores professor) =>
            {
                using var con= await connectionGetter();

                if(professor is null)
                {
                    return Results.BadRequest();
                }
               
                try
                {
                    var id =con.Insert<Professores>(professor);
                    
                    return Results.Created("/professres/id",id);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString() );
                    return Results.StatusCode(500);
                }
                
                
            });

            app.MapPut("/professores", async(GetConnection connectionGetter, Professores professor) =>
            {
                using var con = await connectionGetter();
                if(professor is null)
                {
                    return Results.BadRequest(professor);
                }

                try
                {
                    var retorno = con.Update<Professores>(professor);
                    if (retorno)
                    {
                        return Results.Ok("Atualizado com sucesso");
                    }
                    else
                    {
                        return Results.BadRequest();
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapDelete("/professores/{id}", async(GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                var deleted = con.Get<Professores>(id);
                if(deleted is null)
                {
                    return Results.NotFound();
                }

                try
                {
                    con.Delete<Professores>(deleted);
                    return Results.Ok("Deletado com sucesso!");
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });
        }
        
    }
}
