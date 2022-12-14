using AgendaContatos.Reports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Reports.Interfaces
{
    /// <summary>
    /// Interface para abstração dos métodos de geração de relatório
    /// </summary>
    public interface IContatosReport
    {
        /// <summary>
        /// Método para implementar a geração dos relatórios
        /// </summary>
        byte[] Create(ContatosReportModel model);
    }
}
