using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Data
{
    [Table("Alunos")]
    public record Alunos(
        System.Int32 Id,
        System.String Telefone,
        System.String CPF,
        System.String RG,
        System.String Endereco,
        System.String Telefone_celular,
        System.String Email,
        System.String Nome
    );
}
 