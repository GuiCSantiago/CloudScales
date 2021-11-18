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
    public class CaminhaoController : PadraoController<CaminhaoViewModel>
    {
        public CaminhaoController()
        {
            DAO = new CaminhaoDAO();
            GeraProximoId = true;
        }
        protected override void ValidaDados(CaminhaoViewModel model)
        {
            base.ValidaDados(model);
            if (string.IsNullOrEmpty(model.Placa))
                ModelState.AddModelError("Placa", "Preencha a placa.");
            if (string.IsNullOrEmpty(model.Carreta))
                ModelState.AddModelError("Placa", "Preencha a placa da carreta.");
        }
        public IActionResult ObtemDadosConsultaAvancada(string placa, string carreta)
        {
            try {
                string json = HttpContext.Session.GetString("Logado");
                var model = JsonConvert.DeserializeObject<ClienteViewModel>(json);
                CaminhaoDAO dao = new CaminhaoDAO();
                if (string.IsNullOrEmpty(placa))
                    placa = "";
                if (string.IsNullOrEmpty(carreta))
                    carreta = "";        
                var lista = dao.ConsultaAvancadaCaminhao(placa, carreta, model.Id);
                return PartialView("pvGridCaminhao", lista);
            }
            catch (Exception erro) 
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
    }
}
