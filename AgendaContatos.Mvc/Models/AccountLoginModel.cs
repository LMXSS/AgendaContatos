using AgendaContatos.Mvc.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    /// <summary>
    /// Modelo de dados para o login de usuário
    /// </summary>
    public class AccountLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email de acesso.")]
        public string Email { get; set; }

        [SenhaValidation(ErrorMessage = "Informe uma senha com pelo menos 1 letra maiúscula, 1 letra minúscula, 1 dígito numérico e 1 caractere especial (@,#,$,%,&).")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha de acesso.")]
        public string Senha { get; set; }
    }
}
