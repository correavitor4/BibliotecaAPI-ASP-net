using BibliotecaAPI.Data;
using Dapper.Contrib.Extensions;
using static BibliotecaAPI.Data.BancoContext;

namespace BibliotecaAPI.Endpoints
{
    public static class EmprestimosDeProfessoresEndpoints
    {
        public static void MapEmprestimosDeProfessoresEndpoints(this WebApplication app)
        {
            app.MapGet("/emprestimosDeProfessores", async (GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                try
                {
                    var emprestimos = con.GetAll<EmprestimosProfessores>().ToList();
                    if(emprestimos is null)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(emprestimos);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/emprestimosDeProfessores/{id}", async(GetConnection connectionGetter,int id) =>
            {
                using var con = await connectionGetter();

                try
                {
                    var emprestimo = con.Get<EmprestimosProfessores>(id);
                    if(emprestimo is null)
                    {
                        return Results.NotFound("Nenhum livro com esse Id foi encontrado.");
                    }
                    return Results.Ok(emprestimo);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString() ); ;
                    return Results.StatusCode(500);
                }
            });
            app.MapPost("/emprestimosDeProfessores", async(GetConnection connectionGetter, EmprestimosProfessores emprestimo) =>
            {
                using var con = await connectionGetter();
                if(emprestimo is null)
                {
                    return Results.BadRequest("Você não forneceu nenhum objeto do tipo 'EmprestimosProfessores'");
                }
                try
                {
                    var id = con.Insert<EmprestimosProfessores>(emprestimo);
                    return Results.Created($"/emprestimosDeProfessores/{id}", id);
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }

            });


            app.MapPut("/emprestimosDeProfessores", async (GetConnection connectionGetter,EmprestimosProfessores emprestimo) =>
            {
                using var con = await connectionGetter();

                if(emprestimo is null)
                {
                    return Results.BadRequest("Você não forneceu nenhum objeto para sser atualizado");
                }
                
                /*try
                {
                    var emp = con.Get<EmprestimosProfessores>(emprestimo);
                    if(emp is null)
                    {
                        return Results.NotFound("Esse objeto não foi encontrado. Para atualizá-lo é necessário que ele exista");
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
                */

                try
                {
                    con.Update<EmprestimosProfessores>(emprestimo);
                    return Results.Ok("Atualizado com sucesso!");
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }
            });

            app.MapDelete("/emprestimosDeProfessores/{id}", async(GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                EmprestimosProfessores emp;
                try
                {
                    emp = con.Get<EmprestimosProfessores>(id);
                    if(emp is null)
                    {
                        return Results.NotFound("Não foi possível encontrar nenhum registro de empréstimo para professores com esse id");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return Results.StatusCode(500);
                }


                try
                {
                    con.Delete<EmprestimosProfessores>(emp);
                    return Results.Ok("Empréstimo de professor Deletado com sucesso");
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
