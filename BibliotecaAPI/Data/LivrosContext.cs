
using System.Data;

namespace BibliotecaAPI.Data
{
    public class LivrosContext
    {
        //Aponta para a função que faz a conexão com o banco
        public delegate Task<IDbConnection> GetConnection();
    }
}
