using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    /// <summary>
    /// Modelo de dados para a página de edição de contatos
    /// </summary>
    public class ContatosEdicaoModel
    {
        public Guid IdContato { get; set; } //campo oculto

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do contato.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do contato.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe o telefone do contato.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de nascimento do contato.")]
        public string DataNascimento { get; set; }
    }
}
