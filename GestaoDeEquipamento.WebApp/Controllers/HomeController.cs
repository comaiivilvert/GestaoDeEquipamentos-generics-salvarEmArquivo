using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamento.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
