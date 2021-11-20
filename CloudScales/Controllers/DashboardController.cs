using CloudScales.DAO;
using CloudScales.Models.Exceptions;
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

        public async Task<IActionResult> Equipamento(int id)
        {
            try
            {
                RequisicaoViewModel.Root root = await requisicaoDAO.Requisicao(id);
                return View(root);
            }
            catch (RequisicaoException erro)
            {
                ViewBag.Erro = erro.Message;
                return View("Equipamento");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        [HttpGet]
        public async Task<IActionResult> AtualizaEquipamento(int id)
        {
            try
            {
                var response = await requisicaoDAO.Requisicao(id);

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
