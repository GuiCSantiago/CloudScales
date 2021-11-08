using CloudScales.DAO;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
    }
}
