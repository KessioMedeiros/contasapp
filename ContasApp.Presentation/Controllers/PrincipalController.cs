using ContasApp.Data.Enums;
using ContasApp.Data.Repositories;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        /// <summary>
        /// Método para processar a requisição $.ajax feita pelo Jquery
        /// </summary>
        public JsonResult ConsultarContas(DateTime? dtInicio, DateTime? dtFim)
        {

            try
            {
                var auth = JsonConvert.DeserializeObject<AuthViewModel>(User.Identity.Name);

                var contaRepository = new ContaRepository();
                var contas = contaRepository.GetAll(dtInicio, dtFim, auth.Id);

                //somatório das contas por tipo (receitas e despesas)
                var totalTipos = contas.GroupBy(c => c.Categoria?.Tipo)
                    .Select(c => new
                    {
                        Tipo = c.Key.ToString(), //nome do tipo (Receitas ou Despesas)
                        Total = c.Sum(c => c.Valor) //somatório do valor de cada conta
                    }).ToList();

                //somatório das despesas por categoria
                var totalDespesas = contas.Where(c => c.Categoria?.Tipo == TipoCategoria.Despesas)
                    .GroupBy(c => c.Categoria?.Nome)
                    .Select(c => new
                    {
                        Nome = c.Key.ToString(), //nome da categoria
                        Total = c.Sum(c => c.Valor) //somatório do valor de cada conta
                    }).ToList();

                return Json(new { totalTipos, totalDespesas });
            }
            catch(ArgumentException e)
            {
                return Json("Sucesso");
            }

            
        }
    }
}
