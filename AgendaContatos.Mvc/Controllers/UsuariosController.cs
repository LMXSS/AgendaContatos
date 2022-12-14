using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaContatos.Mvc.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        //ROTA: /Usuarios/SenhaEdicao
        public IActionResult SenhaEdicao()
        {
            return View();
        }

        [HttpPost] //POST -> captura o submit do formulário
        public IActionResult SenhaEdicao(SenhaEdicaoModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //ler o conteudo json gravado no cookie de autenticação do AspNet
                    var json = User.Identity.Name;                    
                    var auth = JsonConvert.DeserializeObject<AuthenticationModel>(json);

                    //verificar se a senha atual está correta
                    var usuarioRepository = new UsuarioRepository();
                    if(usuarioRepository.GetByEmailAndSenha(auth.Email, model.SenhaAtual) != null)
                    {
                        //atualizando a senha do usuário
                        usuarioRepository.Update(auth.IdUsuario, model.NovaSenha);

                        TempData["MensagemSucesso"] = "Sua senha foi atualizada com sucesso, faça um novo login para testar a senha nova.";
                    }
                    else
                    {
                        TempData["MensagemAlerta"] = "Senha atual inválida, por favor verifique.";
                    }
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }
    }
}
