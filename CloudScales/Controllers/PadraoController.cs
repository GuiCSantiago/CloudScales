using CloudScales.DAO;
using CloudScales.Models.Exceptions;
using CloudScales.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCJogos.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        protected string NomeViewBusca { get; set; } = "busca";

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
            var model = JsonConvert.DeserializeObject<ClienteViewModel>(json);
            ViewBag.NomeUser = model.Nome;

            try
            {
                var lista = DAO.Listagem(model.Id);
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

                if (json == string.Empty || json == null)
                    cliente.Id = 1;
                else
                {
                    cliente = JsonConvert.DeserializeObject<ClienteViewModel>(json);
                    ViewBag.NomeUser = cliente.Nome;
                }

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
                    {
                        DAO.Insert(model, Operacao);
                        ViewBag.Sucesso = GetModelName(model) + " cadastrado com sucesso!";
                    }
                    else
                    {
                        DAO.Update(model, Operacao);
                        ViewBag.Sucesso = GetModelName(model) + " foi atualizado com sucesso!";
                    }

                    if (model.GetType().Name == "ClienteViewModel")
                        return RedirectToAction("Index", "Home");

                    return RedirectToAction("Index");
                }
            }
            catch (SqlConcurrencyException erro)
            {
                ViewBag.Operacao = Operacao;
                PreencheDadosParaView(Operacao, model);
                ViewBag.Erro = erro.Message;
                return View(NomeViewForm, model);
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
        public IActionResult Edit()
        {
            string json = HttpContext.Session.GetString("Logado");
            int id = JsonConvert.DeserializeObject<ClienteViewModel>(json).Id;

            try
            {
                ViewBag.Operacao = "A";
                var model = DAO.Consulta(id);
                PreparaListaEquipamentosParaCombo();
                PreparaListaCaminhaoParaCombo();
                ClienteViewModel cliente = new ClienteViewModel();

                if (json == string.Empty || json == null)
                    cliente.Id = 1;
                else
                    cliente = JsonConvert.DeserializeObject<ClienteViewModel>(json);
                ViewBag.ClienteID = cliente.Id;

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
            lista.Add(new SelectListItem("Selecione o caminhão...", "0"));
            foreach (var caminhao in caminhoes)
            {
                SelectListItem item = new SelectListItem(caminhao.Placa, caminhao.Id.ToString());
                lista.Add(item);
            }
            ViewBag.Caminhoes = lista;
        }

        public IActionResult ExibeConsultaAvancada()
        {
            try
            {
                string json = HttpContext.Session.GetString("Logado");
                var model = JsonConvert.DeserializeObject<ClienteViewModel>(json);
                ViewBag.NomeUser = model.Nome;

                return View(NomeViewBusca);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }

        public string GetModelName(T model)
        {
            string modelName = "";

            switch (model.GetType().Name)
            {
                case "CaminhaoViewModel":
                    modelName = "Caminhão";
                    break;
                case "CelulaViewModel":
                    modelName = "Célula";
                    break;
                case "ClienteViewModel":
                    modelName = "Cliente";
                    break;
                case "EquipamentoViewModel":
                    modelName = "Equipamento";
                    break;
                default:
                    modelName = "";
                    break;
            }

            return modelName;
        }

    }
}
