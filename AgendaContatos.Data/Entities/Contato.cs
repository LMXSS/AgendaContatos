using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Entities
{
    /// <summary>
    /// Classe de entidade
    /// </summary>
    public class Contato
    {
        //Propriedades
        public Guid IdContato { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid IdUsuario { get; set; }

        //Relacionamentos (Associações) TER-1
        public Usuario Usuario { get; set; }
    }
}
