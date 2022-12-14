using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    /// <summary>
    /// Modelo de dados para a página de relatório de contatos
    /// </summary>
    public class ContatosRelatorioModel
    {
        [Required(ErrorMessage = "Por favor, escolha o formato do relatório.")]
        public string Formato { get; set; }
    }
}
