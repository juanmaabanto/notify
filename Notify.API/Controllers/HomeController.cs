using Microsoft.AspNetCore.Mvc;

namespace Expertec.Lcc.Services.Notify.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger/");
        }
    }
}