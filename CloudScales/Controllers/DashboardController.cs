using CloudScales.DAO;
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
    public class DashBoardController : Controller
    {
        public CaminhaoDAO DAO;
        public DashBoardController()
        {
            DAO = new CaminhaoDAO();
        }
        public IActionResult Index(int id)
        {
            return View(DAO.Consulta(id));
        }
    }
}
