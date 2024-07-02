using ContasApp.Data.Entities;
using ContasApp.Data.Enums;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        public IActionResult Cadastro()
        {
            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriasCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Capturando os dados do usuario
                    //autenticado através do cookie do AspNet
                    var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                    //preenchendo os dados da categoria
                    //para gravar no banco de dados
                    var categoria = new Categoria
                    {
                        Id = Guid.NewGuid(), // chave primária
                        Nome = model.Nome,
                        Tipo = model.Tipo,
                        UsuarioId = auth?.Id // chave estrangeira
                    };

                    //gravando a categoria no banco de dados
                    var categoriaRepository = new CategoriaRepository();
                    categoriaRepository.Add(categoria);

                    //limapr os campos do formulário
                    ModelState.Clear();

                    TempData["MensagemSucesso"] = $"Categoria  '{categoria.Nome}', cadastrada com sucesso.";
                }
                catch(ArgumentException e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            //carregando uma ViewBag com a opções
            //que serão exibidas no campo DropDownList
            ViewBag.Tipos = new SelectList(Enum.GetValues
            (typeof(TipoCategoria)));
            return View(); //voltando para a página de cadastro
        }

        public IActionResult Consulta()
        {

            var model = new List<CategoriasConsultaViewModel>();

            try
            {
                //caputurando os dados do usuario autenticado
                //através do cookie do aspnet
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                //consultando todas as categorias do usuari autenticado
                var categoriaRepository = new CategoriaRepository();
                foreach(var item in categoriaRepository.GetByUsuario(auth.Id))
                {
                    model.Add(new CategoriasConsultaViewModel
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Tipo = item.Tipo
                    });
                }
            }
            catch(ArgumentException e)
            {
                TempData["MensagemErrro"] = e.Message;
            }
            //enviando a lista para a apagina
            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                //buscar a categoira no banco de dados atraves do ID
                var categoriaRepository = new CategoriaRepository();
                var categoria = categoriaRepository.GetById(id);

                //excluindo a categoria
                categoriaRepository.Delete(categoria);

                TempData["MensagemSucesso"] = $"Categoria '{categoria.Nome}', excluida com sucesso";
            }

            catch(ArgumentException e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(Guid id)
        {
            var model = new CategoriasEdicaoViewModel();

            try
            {
                var categoriaRepository = new CategoriaRepository();
                var categoria = categoriaRepository.GetById(id);

                model.Id = categoria?.Id;
                model.Nome = categoria?.Nome;
                model.Tipo = categoria?.Tipo;
            }
            catch(ArgumentException e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));
            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(CategoriasEdicaoViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //buscar a categoria no banco de dados, atraves do ID
                    var categoriaRepository = new CategoriaRepository();
                    var categoria = categoriaRepository.GetById(model.Id.Value);

                    //Definindo os novos valores
                    categoria.Nome = model.Nome;
                    categoria.Tipo = model.Tipo;

                    //Atualizar a categoria
                    categoriaRepository.Update(categoria);

                    TempData["MensagemSucesso"] = $"Categoria '{categoria.Nome}', atualizada com sucesso.";
                    return RedirectToAction("Consulta");
                }
                catch(ArgumentException e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
             }

            ViewBag.Tipos = new SelectList(Enum.GetValues(typeof(TipoCategoria)));
            return View(model);
        }
    }
}
