using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int clienteID)
        {
            ViewBag.Logado = HelperControllers.VerificaUserLogado(HttpContext.Session);
            ViewBag.clienteID = clienteID;
            return View();
        }
    }
}
