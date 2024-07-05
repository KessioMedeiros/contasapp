using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Abstractions;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        public IActionResult Cadastro()
        {
            ViewBag.Categorias = ObterCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContasCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //capturando os dados do usuario autenticado atraves do cookie do aspnet
                    var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                    //preenchendo os dados da conta
                    var conta = new Conta
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Data = model.Data,
                        Valor = model.Valor,
                        Observacoes = model.Observacoes,
                        CategoriaId = model.CategoriaId,
                        UsuarioId = auth?.Id
                    };

                    //gravando no banco de dados
                    var contaRepository = new ContaRepository();
                    contaRepository.Add(conta);

                    TempData["MensagemSucesso"] = $"Conta '{conta.Nome}', cadastrada com sucesso.";
                    ModelState.Clear();
                }
                catch(ArgumentException e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            ViewBag.Categorias = ObterCategorias();
            return View();
        }

        public IActionResult Consulta()
        {

            var model = new ContasConsultaViewModel();

            //capturando o primeiro e ultimo dia do mes atual
            var dataAtual = DateTime.Now;
            model.DataInicio = new DateTime(dataAtual.Year, dataAtual.Month, 1);
            model.DataFim = model.DataInicio?.AddMonths(1).AddDays(-1);

            model = ConsultarContas(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Consulta(ContasConsultaViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = ConsultarContas(model);
            }

            return View(model);
        }

        private ContasConsultaViewModel ConsultarContas(ContasConsultaViewModel model)
        {
            try
            {
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                var contaRepository = new ContaRepository();
                var contas = contaRepository.GetAll(model.DataInicio, model.DataFim, auth.Id);

                if (contas.Count > 0)
                {

                    model.Resultado = new List<ContasConsultaResultadoViewModel>();
                    foreach (var item in contas)
                    {
                        model.Resultado.Add(new ContasConsultaResultadoViewModel
                        {
                            Id = item.Id,
                            Nome = item.Nome,
                            Data = item.Data,
                            Valor = item.Valor,
                            Categoria = item.Categoria?.Nome,
                            Tipo = item.Categoria?.Tipo.ToString(),
                            observacoes = item.Observacoes
                        });
                    }
                }
                else
                {
                    TempData["MensagemAlerta"] = "Nenhuma conta foi obtida para o período de datas selecionado.";
                }
            }
            catch (ArgumentException e)
            {
                TempData["MesagemErro"] = e.Message;
            }

            return model;
        }

        public IActionResult Edicao(Guid id)
        {

            var model = new ContasEdicaoViewModel();

            try
            {
                var contaRepository = new ContaRepository();
                var conta = contaRepository.GetById(id);

                model.Id = conta.Id;
                model.Nome = conta.Nome;
                model.Data = conta.Data;
                model.Valor = conta.Valor;
                model.Observacoes = conta.Observacoes;
                model.CategoriaId = conta.CategoriaId;


            }
            catch(ArgumentException e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            ViewBag.Categorias = ObterCategorias();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(ContasEdicaoViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var contaRepository = new ContaRepository();
                    var conta = contaRepository.GetById(model.Id.Value);

                    conta.Nome = model.Nome;
                    conta.Valor = model.Valor;
                    conta.Data = model.Data;
                    conta.Observacoes = model.Observacoes;
                    conta.CategoriaId = model.CategoriaId;

                    contaRepository.Update(conta);
                    TempData["MensagemSucesso"] = "Conta atualizada com sucesso.";

                }
                catch(ArgumentException e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            ViewBag.Categorias = ObterCategorias();
            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {

            try
            {
                var contaRepository = new ContaRepository();
                var conta = contaRepository.GetById(id);

                contaRepository.Delete(conta);
                TempData["MensagemSucesso"] = $"Conta '{conta.Nome}', excluida com sucesso.";
            }
            catch(ArgumentException e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        private List<SelectListItem> ObterCategorias()
        {
            var lista = new List<SelectListItem>();

            try
            {
                //capturando os dados do usuario atenticado atraves do cookie do aspnet
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                //consultar as categoiras do usuario autenticado
                var categoriaRepository = new CategoriaRepository();
                var categorias = categoriaRepository.GetByUsuario(auth?.Id); 

                foreach(var item in categorias)
                {
                    lista.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = $"{item.Nome} ({item.Tipo})"
                    });
                }
            }
            catch(ArgumentException e)
            {
                TempData["MenssagemErro"] = e.Message;
            }

            return lista;
        }
        
    }
}
