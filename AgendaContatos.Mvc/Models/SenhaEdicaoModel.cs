using AgendaContatos.Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    /// <summary>
    /// Modelo de dados para o formulário da página de edição de senha
    /// </summary>
    public class SenhaEdicaoModel
    {
        [Required(ErrorMessage = "Por favor, informe sua senha atual.")]
        public string SenhaAtual { get; set; }

        [SenhaValidation(ErrorMessage = "Informe uma senha com pelo menos 1 letra maiúscula, 1 letra minúscula, 1 dígito numérico e 1 caractere especial (@,#,$,%,&).")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe sua nova senha.")]
        public string NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme sua nova senha.")]
        public string NovaSenhaConfirmacao { get; set; }
    }
}
