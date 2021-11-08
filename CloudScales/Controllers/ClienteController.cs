using CloudScales.DAO;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Controllers
{
    public class ClienteController : PadraoController<ClienteViewModel>
    {
        public ClienteController()
        {
            DAO = new ClienteDAO();
            GeraProximoId = true;
        }
        protected override void ValidaDados(ClienteViewModel model)
        {
            base.ValidaDados(model);
            if (String.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome!");
            if (model.CNPJ <= 0)
                ModelState.AddModelError("CNPJ", "Preencha o CNPJ!");
            if (String.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "Preencha o e-mail!");
            if (String.IsNullOrEmpty(model.Senha))
                ModelState.AddModelError("Senha", "Preencha a senha!");
        }
    }
}
