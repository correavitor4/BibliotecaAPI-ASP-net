using System.ComponentModel.DataAnnotations.Schema;
namespace BibliotecaAPI.Data
{   [Table("Autores")]
    public record Autores
    (
        System.Int32 Id,
        System.String Nome
    );
        

    
}
