
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Data
{
    [Table("EmprestimosDeAlunos")]
    public record EmprestimosDeAlunos
    (
        System.Int32 Fk_aluno,
        System.Int32 Exemplar_fk, 
        System.DateTime Data_prevista_devolucao, 
        System.DateTime Data_emprestimo, 
        System.Int32 Id
    );
        
        
    
}
