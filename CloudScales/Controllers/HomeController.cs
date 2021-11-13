using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            string json = HttpContext.Session.GetString("Logado");

            ViewBag.Logado = HelperControllers.VerificaUserLogado(HttpContext.Session);
            ViewBag.clienteID = clienteID;
            ViewBag.NomeUser = JsonConvert.DeserializeObject<ClienteViewModel>(json).Nome;
            return View();
        }
    }
}
