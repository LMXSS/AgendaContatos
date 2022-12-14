using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Settings
{
    /// <summary>
    /// Classe para fornecer os parametros de conexão de banco de dados
    /// para a camada de repositório de dados
    /// </summary>
    public class ConnectionSettings
    {
        //método estático para retornar a connectionstring
        public static string GetConnectionString()
        {
            return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDAgenda;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
