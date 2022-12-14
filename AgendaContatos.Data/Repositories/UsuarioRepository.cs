using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Settings;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Repositories
{
    /// <summary>
    /// Classe de repositório de dados para a entidade Usuário
    /// </summary>
    public class UsuarioRepository
    {
        //método para inserir um usuário no banco de dados
        public void Create(Usuario usuario)
        {
            var sql = @"
                    INSERT INTO USUARIO(
                        IDUSUARIO,
                        NOME,
                        EMAIL,
                        SENHA,
                        DATACADASTRO)
                    VALUES(
                        @IdUsuario,
                        @Nome,
                        @Email,
                        CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2),
                        @DataCadastro)
                ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                connection.Execute(sql, usuario);
            }
        }

        //método para atualizar a senha do usuário
        public void Update(Guid idUsuario, string novaSenha)
        {
            var sql = @"
                UPDATE USUARIO 
                SET SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @novaSenha), 2) 
                WHERE IDUSUARIO = @idUsuario
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                connection.Execute(sql, new { idUsuario, novaSenha });
            }
        }

        //método para consultar 1 usuário no banco de dados através do email
        public Usuario GetByEmail(string email)
        {
            var sql = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                return connection
                    .Query<Usuario>(sql, new { email })
                    .FirstOrDefault();
            }
        }

        //método para consultar 1 usuário no banco de dados através do email e senha
        public Usuario GetByEmailAndSenha(string email, string senha)
        {
            var sql = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
                AND SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @senha), 2)
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                return connection
                    .Query<Usuario>(sql, new { email, senha })
                    .FirstOrDefault();
            }
        }
    }
}
