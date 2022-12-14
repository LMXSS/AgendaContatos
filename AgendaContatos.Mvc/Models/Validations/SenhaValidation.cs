using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models.Validations
{
    /// <summary>
    /// Classe de validação customizada
    /// </summary>
    public class SenhaValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value != null && value is string)
            {
                var senha = value as string;

                return senha.Any(s => char.IsLower(s)) //pelo menos 1 letra minúscula
                    && senha.Any(s => char.IsUpper(s)) //pelo menos 1 letra maiuscula
                    && senha.Any(s => char.IsDigit(s)) //pelo menos 1 número
                    && ( //pelo menos 1 dos caracteres especiais abaixo
                        senha.Contains("@") ||
                        senha.Contains("$") ||
                        senha.Contains("%") ||
                        senha.Contains("#") ||
                        senha.Contains("&")
                    );
            }

            return false;
        }
    }
}
