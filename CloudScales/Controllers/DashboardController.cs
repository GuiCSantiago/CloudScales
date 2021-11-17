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

        public async Task<IActionResult> Equipamento()
        {
            return View(await requisicaoDAO.Requisicao());
        }

        [HttpGet]
        public async Task<IActionResult> AtualizaEquipamento()
        {
            try
            {
                var response = await requisicaoDAO.Requisicao();

                return Json(new
                {
                    dados = response,
                    sucesso = true,
                    tipo = true ? "sucesso" : "alerta",
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    mensagem = "Erro ao atualizar equipamento = " + ex.Message,
                    sucesso = false,
                    tipo = "erro",
                });
            }
        }
    }
}
