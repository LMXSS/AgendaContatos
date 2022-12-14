using AgendaContatos.Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    /// <summary>
    /// Modelo de dados para cadastro de conta de usuário
    /// </summary>
    public class AccountRegisterModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o seu nome completo.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe seu endereço de email.")]
        public string Email { get; set; }

        [SenhaValidation(ErrorMessage = "Informe uma senha com pelo menos 1 letra maiúscula, 1 letra minúscula, 1 dígito numérico e 1 caractere especial (@,#,$,%,&).")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe sua senha de acesso.")]
        public string Senha { get; set; }
        
        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme sua senha de acesso.")]
        public string SenhaConfirmacao { get; set; }
    }
}
