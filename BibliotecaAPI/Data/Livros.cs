using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Data
{
    [Table("Livros")]
    public record Livros(
        System.String Titulo, 
        System.String Editora, 
        System.String Edicao, 
        System.Int32 Ano_de_publicacao, 
        System.String Assunto, 
        System.Int32 Id
    );
    
}
