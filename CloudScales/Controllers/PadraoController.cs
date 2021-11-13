using CloudScales.DAO;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCJogos.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Controllers
{
    public class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GeraProximoId { get; set; }
        protected string NomeViewIndex { get; set; } = "index";
        protected string NomeViewForm { get; set; } = "form";
        protected bool ExigeAutenticacao { get; set; } = true;
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ExigeAutenticacao && !HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        public virtual IActionResult Index()
        {
            string json = HttpContext.Session.GetString("Logado");
            ViewBag.NomeUser = JsonConvert.DeserializeObject<ClienteViewModel>(json).Nome;

            try
            {
                var lista = DAO.Listagem();
                return View(NomeViewIndex, lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public virtual IActionResult Create()
        {
            try
            {
                string json = HttpContext.Session.GetString("Logado");
                ClienteViewModel cliente = new ClienteViewModel();

                if(json == string.Empty || json == null)
                    cliente.Id = 1;
                else
                    cliente = JsonConvert.DeserializeObject<ClienteViewModel>(json);

                ViewBag.ClienteID = cliente.Id;
                ViewBag.Operacao = "I";
                T model = Activator.CreateInstance(typeof(T)) as T;
                PreencheDadosParaView("I", model);
                PreparaListaEquipamentosParaCombo();
                PreparaListaCaminhaoParaCombo();
                return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        protected virtual void PreencheDadosParaView(string Operacao, T model)
        {
            if (GeraProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
        }
        public virtual IActionResult Save(T model, string Operacao, int clienteId)
        {
            try
            {
                ValidaDados(model);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    PreparaListaEquipamentosParaCombo();
                    PreparaListaCaminhaoParaCombo();
                    return View(NomeViewForm, model);
                }
                else
                {
                    if (Operacao == "I")
                        DAO.Insert(model, Operacao);
                    else
                        DAO.Update(model, Operacao);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        protected virtual void ValidaDados(T model)
        {
            ModelState.Clear();
        }
        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                var model = DAO.Consulta(id);
                PreparaListaEquipamentosParaCombo();
                PreparaListaCaminhaoParaCombo();
                if (model == null)
                    return RedirectToAction(NomeViewIndex);
                else
                {
                    PreencheDadosParaView("A", model);
                    return View(NomeViewForm, model);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);
                return RedirectToAction(NomeViewIndex);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        private void PreparaListaEquipamentosParaCombo()
        {
            EquipamentoDAO dao = new EquipamentoDAO();
            var equipamentos = dao.ListaEquipamento();
            List<SelectListItem> listaEquip = new List<SelectListItem>();
            listaEquip.Add(new SelectListItem("Selecione a Balança...", "0"));
            foreach (var equip in equipamentos)
            {
                SelectListItem item = new SelectListItem(equip.Id.ToString(), equip.Id.ToString());
                listaEquip.Add(item);
            }
            ViewBag.Equipamentos = listaEquip;
        }

        private void PreparaListaCaminhaoParaCombo()
        {
            CaminhaoDAO dao = new CaminhaoDAO();
            var caminhoes = dao.ListaCaminhao();
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem("Selecione a Balança...", "0"));
            foreach (var caminhao in caminhoes)
            {
                SelectListItem item = new SelectListItem(caminhao.Placa, caminhao.Id.ToString());
                lista.Add(item);
            }
            ViewBag.Caminhoes = lista;
        }
    }
}
