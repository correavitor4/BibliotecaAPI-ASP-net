
using System.Data;

namespace BibliotecaAPI.Data
{
    public class BancoContext
    {
        //Aponta para a função que faz a conexão com o banco
        public delegate Task<IDbConnection> GetConnection();
    }
}
