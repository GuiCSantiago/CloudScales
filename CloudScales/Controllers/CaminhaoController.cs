using CloudScales.DAO;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
    }
}
