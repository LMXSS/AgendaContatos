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
    public class ContatoRepository
    {
        public void Create(Contato contato)
        {
            var sql = @"
                INSERT INTO CONTATO(
                    IDCONTATO,
                    NOME,
                    EMAIL,
                    TELEFONE,
                    FOTO,
                    DATANASCIMENTO,
                    IDUSUARIO)
                VALUES(
                    @IdContato,
                    @Nome,
                    @Email,
                    @Telefone,
                    @Foto,
                    @DataNascimento,
                    @IdUsuario
                )
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                connection.Execute(sql, contato);
            }
        }

        public void Update(Contato contato)
        {
            var sql = @"
                UPDATE CONTATO
                SET
                    NOME = @Nome,
                    EMAIL = @Email,
                    TELEFONE = @Telefone,
                    FOTO = @Foto,
                    DATANASCIMENTO = @DataNascimento
                WHERE
                    IDCONTATO = @IdContato
                AND
                    IDUSUARIO = @IdUsuario
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                connection.Execute(sql, contato);
            }
        }

        public void Delete(Contato contato)
        {
            var sql = @"
                DELETE FROM CONTATO
                WHERE
                    IDCONTATO = @IdContato
                AND
                    IDUSUARIO = @IdUsuario
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                connection.Execute(sql, contato);
            }
        }

        public List<Contato> GetByUsuario(Guid idUsuario)
        {
            var sql = @"
                SELECT * FROM CONTATO c
                INNER JOIN USUARIO u
                ON u.IDUSUARIO = c.IDUSUARIO
                WHERE c.IDUSUARIO = @idUsuario
                ORDER BY c.NOME ASC
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                return connection
                        .Query(sql, (Contato c, Usuario u) =>
                        {
                            c.Usuario = u;
                            return c;
                        },
                        new { idUsuario },
                        splitOn: "IdUsuario")
                        .ToList();
            }
        }

        public Contato GetById(Guid idContato, Guid idUsuario)
        {
            var sql = @"
                SELECT * FROM CONTATO c
                INNER JOIN USUARIO u
                ON u.IDUSUARIO = c.IDUSUARIO
                WHERE c.IDCONTATO = @idContato
                AND c.IDUSUARIO = @idUsuario
            ";

            using (var connection = new SqlConnection(ConnectionSettings.GetConnectionString()))
            {
                return connection
                        .Query(sql, (Contato c, Usuario u) =>
                        {
                            c.Usuario = u;
                            return c;
                        },
                        new { idContato, idUsuario },
                        splitOn: "IdUsuario")
                        .FirstOrDefault();
            }
        }
    }
}
