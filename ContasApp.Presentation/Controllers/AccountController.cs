using Bogus;
using ContasApp.Data.Entities;
using ContasApp.Data.Repositories;
using ContasApp.Messagens.Models;
using ContasApp.Messagens.Services;
using ContasApp.Presentation.Helpers;
using ContasApp.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace ContasApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model) 
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //Verificando seu o email já está cadastrado
                    //Passaram as regras de validação
                    var usuarioRepository = new UsuarioRepository();
                    if(usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        ModelState.AddModelError("Email", "O email informado já está cadastrado para outro usuario.");
                    }
                    else
                    {
                        //Criando objeto usuario
                        var usuario = new Usuario()
                        {
                            Id = Guid.NewGuid(),
                            Nome = model.Nome,
                            Email = model.Email,
                            Senha = MD5Helper.Encrypt(model.Senha)
                        };

                        //Gravando o usuario no banco de dados
                        usuarioRepository.Add(usuario);
                        //Gerando mensagem
                        TempData["Mensagem"] = "Parabéns, sua conta de usuário foi criada com sucesso.";
                        //Limpar os campos do formulario
                        ModelState.Clear();
                    }
                }
                catch(ArgumentException e)
                {
                    TempData["Mensagem"] = e.Message;
                }

            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Buscar o usuario no banco de dados
                    //Atraves do email e da senha
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, MD5Helper.Encrypt(model.Senha));

                    //Verificar se o usuario foi encontrado
                    if(usuario != null)
                    {
                        //criando os dados que serão gravados 
                        //no cookie para autenticação do usuario
                        var auth = new AuthViewModel
                        {
                            Id = usuario.Id,
                            Nome = usuario.Nome,
                            Email = usuario.Email,
                            DataHoraAcesso = DateTime.Now
                        };

                        //serializando o objeto AuthViewModel para JSON
                        var authJson = JsonConvert.SerializeObject(auth);

                        //criando o conteúdo do cookie
                        //de autenticação (Identificação)
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, authJson) }, CookieAuthenticationDefaults.AuthenticationScheme);

                        //gravando o cookie de autentificação
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Dashboard", "Principal");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado";
                    }
                }
                catch(ArgumentException e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }
            return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PasswordRecover(AccountPasswordRecoverViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    if(usuario != null)
                    {

                        Faker faker = new Faker();
                        var novaSenha = $"@{faker.Internet.Password(8)} {new Random().Next(999)}";

                        var emailMessageModel = new EmailMessageModel
                        {
                            EmailDestinatario = usuario.Email,
                            Assunto = "Recuperação de senha de usuario - Contas App",
                            Corpo = $"Prezado, {usuario.Nome}, \nSua nova senha de acesso é: {novaSenha}\nAtt\nEquipe Contas App"
                        };

                        EmailMessageService.Send(emailMessageModel);

                        usuario.Senha = MD5Helper.Encrypt(novaSenha);
                        usuarioRepository.Update(usuario);

                        TempData["Mensagem"] = "Recuperação de senha realizada com sucesso. Verifique sua caixa de email.";
                        ModelState.Clear();
                    }
                    else
                    {
                        TempData["Mensagem"] = "usuario não foi encontrado";
                    }

                }
                catch(ArgumentException e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            //apagar o cookie de autentificação
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar para a pagina /Account/Login
            return RedirectToAction("Login", "Account");

        }
    }
}
