using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Data
{
    [Table("EmprestimosDeProfessores")]
    public record EmprestimosProfessores(
        System.Int32 Id,
        System.Int32 Fk_professor,
        System.DateTime Data_do_emprestimo,
        System.DateTime Data_prevista_devolucao,
        System.Int32 Exemplar_emprestado 
    );
}
