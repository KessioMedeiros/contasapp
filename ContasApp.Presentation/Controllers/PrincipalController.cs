using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContasApp.Presentation.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
