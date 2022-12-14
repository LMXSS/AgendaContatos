using AgendaContatos.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Reports.Models
{
    /// <summary>
    /// Modelo de dados para preenchimento do relatório de contatos
    /// </summary>
    public class ContatosReportModel
    {
        public DateTime? DataHora { get; set; }
        public Usuario? Usuario { get; set; }
        public List<Contato>? Contatos { get; set; }
    }
}
