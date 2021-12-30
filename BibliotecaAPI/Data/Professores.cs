using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Data
{
    [Table("Professores")]
    public record Professores(
        System.String Nome,
        System.String Endereco,
        System.String Telefone,
        System.String Telefone_celular,
        System.String CPF,
        System.String RG,
        System.String Email,
        System.Int32 Id
        );
}
