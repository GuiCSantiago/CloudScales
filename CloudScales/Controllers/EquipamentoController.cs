using CloudScales.DAO;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Controllers
{
    public class EquipamentoController : PadraoController<EquipamentoViewModel>
    {
        public EquipamentoController()
        {
            DAO = new EquipamentoDAO();
            GeraProximoId = true;
        }
        protected override void ValidaDados(EquipamentoViewModel model)
        {
            base.ValidaDados(model);
            if (model.CaminhaoID <= 0)
                ModelState.AddModelError("CaminhaoID", "Selecione o caminhão!");
            if (model.QtdBalanca <= 0)
                ModelState.AddModelError("QtdBalanca", "Preencha a quantidade!");
        }

        public IActionResult ObtemDadosConsultaAvancadaEquipamento(string caminhaoID, string qtdbalanca)
        {
            try
            {
                int id = 1;
                string json = HttpContext.Session.GetString("Logado");
                if (json != string.Empty || json != null)
                    id = JsonConvert.DeserializeObject<ClienteViewModel>(json).Id;
                EquipamentoDAO dao = new EquipamentoDAO();
                if (string.IsNullOrEmpty(caminhaoID))
                    caminhaoID = "";
                if (string.IsNullOrEmpty(qtdbalanca))
                    qtdbalanca = "";
                var lista = dao.ConsultaAvancadaEquipamento(caminhaoID, qtdbalanca, id);
                return PartialView("pvGridEquipamento", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }

        public async Task<bool> ImportaEntidade(int id)
        {
            EquipamentoDAO equipamentoDAO = new EquipamentoDAO();
            EquipamentoViewModel equipamento = equipamentoDAO.Consulta(id);
            RequisicaoDAO requisicaoDAO = new RequisicaoDAO();

            return await requisicaoDAO.CriarEntidade(equipamento);
        }
    }
}
