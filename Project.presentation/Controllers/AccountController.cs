using Microsoft.AspNetCore.Mvc;

namespace Project.presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register() => View();
    }
}
