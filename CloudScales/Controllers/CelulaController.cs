using CloudScales.DAO;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Controllers
{
    public class CelulaController : PadraoController<CelulaViewModel>
    {
        public CelulaController()
        {
            DAO = new CelulaDAO();
            GeraProximoId = true;
        }
        protected override void ValidaDados(CelulaViewModel model)
        {
            base.ValidaDados(model);
            if (model.EquipamentoID <= 0)
                ModelState.AddModelError("EquipamentoID", "Selecione o equipamento");
            if (model.Posicao <= 0)
                ModelState.AddModelError("Posicao", "Posição inválida.");
            if (model.Peso <= 0)
                ModelState.AddModelError("Peso", "Peso inválido.");
        }
        public IActionResult ObtemDadosConsultaAvancadaCelula(string equipamentoID, string peso, string posicao)
        {
            try
            {
                CelulaDAO dao = new CelulaDAO();
                if (string.IsNullOrEmpty(equipamentoID))
                    equipamentoID = "";
                if (string.IsNullOrEmpty(peso))
                    peso = "";
                if (string.IsNullOrEmpty(posicao))
                    posicao = "";
                var lista = dao.ConsultaAvancadaCelula(equipamentoID, peso, posicao);
                return PartialView("pvGridCelula", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
    }
    
}
