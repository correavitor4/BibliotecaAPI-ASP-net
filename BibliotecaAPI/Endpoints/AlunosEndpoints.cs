using BibliotecaAPI.Data;
using Dapper.Contrib.Extensions;
using static BibliotecaAPI.Data.BancoContext;

namespace BibliotecaAPI.Endpoints
{
    public static class AlunosEndpoints
    {
        public static void MapAlunosEndpoints(this WebApplication app)
        {
            app.MapGet("/alunos", async(GetConnection connectionGetter)=>
            {
                using var con = await connectionGetter();
                List<Alunos> alunos = new List<Alunos>();
                try
                {
                    alunos= con.GetAll<Alunos>().ToList();
                    if(alunos is null)
                    {
                        return Results.NotFound("Não foi possível encontrar nenhum autor");
                    }
                    return Results.Ok(alunos);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/alunos/{id}", async(GetConnection connectionGetter, int id)=>
            {
                using var con = await connectionGetter();
                try
                {
                    Alunos aluno = con.Get<Alunos>(id);
                    if (aluno is null)
                    {
                        return Results.NotFound("O aluno solicitado não foi encontrado. Por favorm verifique o Id");

                    }

                    return Results.Ok(aluno);
                }
                catch   (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }

            });

            app.MapPost("/alunos", async(GetConnection connectionGetter, Alunos aluno) =>
            {
                using var con = await connectionGetter();
                if(aluno is null)
                {
                    return Results.BadRequest("O aluno não foi fornecido");
                }

                try
                {
                    var id = con.Insert<Alunos>(aluno);

                    return Results.Created($"/alunos/{id}", id);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapPut("/alunos", async(GetConnection connectionGetter, Alunos aluno) =>
            {
                using var con = await connectionGetter();
                try
                {
                    con.Update<Alunos>(aluno);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapDelete("/alunos/{id}", async(GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                Alunos aluno = con.Get<Alunos>(id); 
                if(aluno is null)
                {
                    return Results.NotFound("O aluno não foi encontrado. Por favor, verifique o Id");
                }

                try
                {
                    con.Delete<Alunos>(aluno);
                    return Results.Ok("Deletado com sucesso");
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
