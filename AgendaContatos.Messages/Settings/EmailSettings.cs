using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Messages.Settings
{
    /// <summary>
    /// Parâmetros necessários para envio de email
    /// </summary>
    public static class EmailSettings
    {
        public static string Conta => "cotinaoresponda@outlook.com";
        public static string Senha => "@Admin123456";
        public static string Smtp => "smtp-mail.outlook.com";
        public static int Porta => 587;
    }
}
