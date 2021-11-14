﻿using CloudScales.Models.ViewModels;
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
        public IActionResult Index()
        {
            string json = HttpContext.Session.GetString("Logado");
            ViewBag.Logado = HelperControllers.VerificaUserLogado(HttpContext.Session);
            if(ViewBag.Logado)
                ViewBag.NomeUser = JsonConvert.DeserializeObject<ClienteViewModel>(json).Nome;
            else
                return RedirectToAction("index", "Login");
            return View();
        }
    }
}
