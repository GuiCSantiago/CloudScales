using CloudScales.DAO;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RequisicaoViewModel;

namespace CloudScales.Controllers
{
    public class DashBoardController : Controller
    {
        public CaminhaoDAO caminhaoDAO;
        public RequisicaoDAO requisicaoDAO;
        public DashBoardController()
        {
            caminhaoDAO = new CaminhaoDAO();
            requisicaoDAO = new RequisicaoDAO();
        }

        public IActionResult Index(int id)
        {
            return View(caminhaoDAO.Consulta(id));
        }

        public IActionResult Equipamento()
        {
            return View(requisicaoDAO.Requisicao());
        }

        public async Task<Root> AtualizaEquipamento()
        {
            return await requisicaoDAO.Requisicao();
        }
    }
}
