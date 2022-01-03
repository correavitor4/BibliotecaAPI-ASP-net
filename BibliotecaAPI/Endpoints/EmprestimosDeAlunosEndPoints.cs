using BibliotecaAPI.Data;
using Dapper.Contrib.Extensions;
using static BibliotecaAPI.Data.BancoContext;

namespace BibliotecaAPI.Endpoints
{
    public static class EmprestimosDeAlunosEndPoints
    {
        public static void MapEmprestimosDeAlunosEndPoints(this WebApplication app)
        {
            app.MapGet("/emprestimosDeAlunos", async(GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                try
                {
                    var emps = con.GetAll<EmprestimosDeAlunos>().ToList();
                    if(emps is null)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(emps);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapPost("/emprestimosDeAlunos", async (GetConnection connectionGetter, EmprestimosDeAlunos emprestimo) =>
            {
                using var con = await connectionGetter();
                
                
                
                try
                {
                    var id=con.Insert<EmprestimosDeAlunos>(emprestimo);
                    
                    return Results.Created($"/emprestimosDeAlunos/{id}",id);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/emprestimosDeAlunos/{id}", async (GetConnection connectionGetter,int id) =>
            {
                using var con = await connectionGetter();
                try
                {
                    EmprestimosDeAlunos emp = con.Get<EmprestimosDeAlunos>(id);
                    if (emp is null)
                    {
                        return Results.NotFound("Não foi encontrado nenhum empréstimo com esse Id");
                    }
                    return Results.Ok(emp);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
                
            });

            app.MapPut("/emprestimosDeAlunos", async(GetConnection connectionGetter, EmprestimosDeAlunos emp) =>
            {
                using var con = await connectionGetter();
                if(emp is null)
                {
                    return Results.BadRequest("O objeto enviado é nulo");
                }
                try
                {
                    
                    var response = con.Update<EmprestimosDeAlunos>(emp);
                    if(response is false)
                    {
                        return Results.StatusCode(500);
                    }

                    return Results.Ok("Empréstimo atualizado com sucesso");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapDelete("/emprestimosDeAlunos/{id}", async (GetConnection connectionGetter,int id) =>
            {
                using var con = await connectionGetter();

                EmprestimosDeAlunos emprestimo;
                try
                {
                    emprestimo = con.Get<EmprestimosDeAlunos>(id);
                    if(emprestimo is null)
                    {
                        return Results.NotFound("Não foi encontrado nenhum empréstimo com esse Id");
                    }

                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }


                try
                {
                    var response = con.Delete<EmprestimosDeAlunos>(emprestimo);
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });
        }
        
       
    }
}
