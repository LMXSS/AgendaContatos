using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using AgendaContatos.Reports.Interfaces;
using AgendaContatos.Reports.Models;
using AgendaContatos.Reports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaContatos.Mvc.Controllers
{
    [Authorize]
    public class ContatosController : Controller
    {
        //ROTA: /Contatos/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] //SUBMIT POST do formulário
        public IActionResult Cadastro(ContatosCadastroModel model, [FromServices] IWebHostEnvironment environment)
        {
            //verificar se todos os campos passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    //capturar os dados do contato para gravar no banco de dados
                    var contato = new Contato()
                    {
                        IdContato = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        DataNascimento = Convert.ToDateTime(model.DataNascimento),
                        Telefone = model.Telefone,
                        Foto = "/img/usuarios/avatar.png",
                        IdUsuario = GetUsuarioAutenticado().IdUsuario
                    };

                    //verificar se foi enviado uma foto do contato (upload)
                    UploadFoto(contato, environment);

                    //gravando no banco de dados
                    var contatoRepository = new ContatoRepository();
                    contatoRepository.Create(contato);

                    TempData["MensagemSucesso"] = $"Contato {contato.Nome}, cadastrado com sucesso em sua agenda.";
                    ModelState.Clear(); //limpar os campos do formulário
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao cadastrar contato: {e.Message}";
                }
            }

            return View();
        }

        //ROTA: /Contatos/Consulta
        public IActionResult Consulta()
        {
            //criando uma lista da classe ContatosConsultaModel
            var lista = new List<ContatosConsultaModel>();

            try
            {
                //consultar os contatos do usuário autenticado na agenda
                var contatoRepository = new ContatoRepository();
                foreach (var item in contatoRepository.GetByUsuario(GetUsuarioAutenticado().IdUsuario))
                {
                    var model = new ContatosConsultaModel()
                    {
                        IdContato = item.IdContato,
                        Nome = item.Nome,
                        Email = item.Email,
                        Telefone = item.Telefone,
                        DataNascimento = item.DataNascimento.ToString("dd/MM/yyyy"),
                        Foto = item.Foto
                    };

                    lista.Add(model);
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao consultar contatos: {e.Message}";
            }

            return View(lista); //enviando a lista para a página
        }

        //ROTA: /Contatos/Exclusao/id
        public IActionResult Exclusao(Guid id, [FromServices] IWebHostEnvironment environment)
        {
            try
            {
                //acessando o repositório
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id, GetUsuarioAutenticado().IdUsuario);

                //excluindo a foto do contato
                if( ! contato.Foto.Equals("/img/usuarios/avatar.png"))
                    System.IO.File.Delete(environment.WebRootPath + contato.Foto);

                //excluindo o contato
                contatoRepository.Delete(contato);
                TempData["MensagemSucesso"] = $"Contato {contato.Nome} excluído com sucesso.";
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao excluir o contato: {e.Message}";
            }

            //redirecionar para a página de consulta
            return RedirectToAction("Consulta");
        }

        //ROTA: /Contatos/Edicao/id
        public IActionResult Edicao(Guid id)
        {
            var model = new ContatosEdicaoModel();

            try
            {
                //buscando no banco de dados o contato através do ID
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id, GetUsuarioAutenticado().IdUsuario);

                //preencher a model com os dados do contato
                model.IdContato = contato.IdContato;
                model.Nome = contato.Nome;
                model.Email = contato.Email;
                model.Telefone = contato.Telefone;
                model.DataNascimento = contato.DataNascimento.ToString("dd/MM/yyyy");
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Falha ao editar o contato: {e.Message}";
            }

            //enviando a model para a página
            return View(model);
        }

        [HttpPost] //SUBMIT POST do formulário
        public IActionResult Edicao(ContatosEdicaoModel model, [FromServices] IWebHostEnvironment environment)
        {
            //verificar se todos os campos passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    var contatoRepository = new ContatoRepository();

                    //capturar os dados do contato
                    var contato = contatoRepository.GetById(model.IdContato, GetUsuarioAutenticado().IdUsuario);

                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone = model.Telefone;
                    contato.DataNascimento = Convert.ToDateTime(model.DataNascimento);

                    //verificar se foi enviado uma foto do contato (upload)
                    UploadFoto(contato, environment);

                    //atualizando no banco de dados                    
                    contatoRepository.Update(contato);

                    TempData["MensagemSucesso"] = $"Contato {contato.Nome} atualizado com sucesso.";
                    return RedirectToAction("Consulta"); //redirecionamento
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao editar o contato: {e.Message}";
                }
            }

            return View(model);
        }

        //ROTA: /Contatos/Relatorio
        public IActionResult Relatorio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Relatorio(ContatosRelatorioModel model)
        {
            //verificar se os dados passaram nas regras de validação
            if(ModelState.IsValid)
            {
                var usuarioRepository = new UsuarioRepository();
                var contatoRepository = new ContatoRepository();    

                try
                {
                    //criar o modelo de dados para geração do relatório
                    var contatosReportModel = new ContatosReportModel()
                    {
                        DataHora = DateTime.Now,
                        Usuario = usuarioRepository.GetByEmail(GetUsuarioAutenticado().Email),
                        Contatos = contatoRepository.GetByUsuario(GetUsuarioAutenticado().IdUsuario)
                    };

                    //polimorfismo
                    IContatosReport contatosReport = null;
                    var nomeArquivo = string.Empty;
                    var tipoArquivo = string.Empty;

                    switch(model.Formato)
                    {
                        case "excel": 
                            contatosReport = new ContatosReportExcel();
                            nomeArquivo = $"contatos_{Guid.NewGuid()}.xlsx";
                            tipoArquivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            break;

                        case "pdf": 
                            contatosReport = new ContatosReportPdf();
                            nomeArquivo = $"contatos_{Guid.NewGuid()}.pdf";
                            tipoArquivo = "application/pdf";
                            break;
                    }

                    //download do relatório
                    return File(contatosReport.Create(contatosReportModel), tipoArquivo, nomeArquivo);
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao gerar relatório: {e.Message}";
                }
            }

            return View();
        }

        private void UploadFoto(Contato contato, IWebHostEnvironment environment)
        {
            //verificar se foi enviado uma foto do contato (upload)
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var extensao = Path.GetExtension(file.FileName);
                if (extensao.Equals(".jpg") || extensao.Equals(".jpeg") || extensao.Equals(".png"))
                {
                    //caminho da foto no banco de dados
                    contato.Foto = $"/img/contatos/{contato.IdContato}.png";

                    //salvar a foto dentro da pasta /wwwroot
                    using (var stream = new FileStream(environment.WebRootPath + contato.Foto, FileMode.Create))
                    {
                        file.CopyTo(stream); //upload do arquivo!
                    }
                }
            }
        }

        //método no controlador que retorne os dados do usuário autenticado
        //no AspNet MVC (Cookie de autenticação)
        private AuthenticationModel GetUsuarioAutenticado()
        {
            //ler os dados do usuário autenticado que estão gravados
            //no arquivo de cookie do AspNet (em formato JSON)
            var json = User.Identity.Name;

            //deserializar e retornar estes dados na forma de objeto
            return JsonConvert.DeserializeObject<AuthenticationModel>(json);
        }
    }
}
