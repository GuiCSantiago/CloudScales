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
    }
}
