﻿using CloudScales.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.DAO
{
    public class EquipamentoDAO : PadraoDAO<EquipamentoViewModel>
    {
        protected override SqlParameter[] CriaParametros(EquipamentoViewModel model, string operacao)
        {
            SqlParameter[] parametros;
            if (operacao != "I")
            {
                parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("Id", model.Id);
                parametros[1] = new SqlParameter("CaminhaoID", model.CaminhaoID);
                parametros[2] = new SqlParameter("QtdBalanca", model.QtdBalanca);
            }
            else
            {
                parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("CaminhaoID", model.CaminhaoID);
                parametros[1] = new SqlParameter("QtdBalanca", model.QtdBalanca);
            }
            return parametros;
        }

        protected override EquipamentoViewModel MontaModel(DataRow registro)
        {
            EquipamentoViewModel model = new EquipamentoViewModel();
            model.Id = Convert.ToInt32(registro["Id"]);
            model.CaminhaoID = Convert.ToInt32(registro["CaminhaoID"]);
            model.QtdBalanca = Convert.ToInt32(registro["QtdBalanca"]);
            return model;
        }
        protected override void SetTabela()
        {
            Tabela = "Equipamento";
        }

        public List<EquipamentoViewModel> ListaEquipamento(int id)
        {
            SqlParameter[] parametro = { new SqlParameter("id", id) };
            List<EquipamentoViewModel> lista = new List<EquipamentoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemEquipamento", parametro);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }

        public List<EquipamentoViewModel> ConsultaAvancadaEquipamento(string caminhaoID, string qtdBalanca, int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("caminhaoID", caminhaoID),
                new SqlParameter("qtdBalanca", qtdBalanca)
            };
            List<EquipamentoViewModel> lista = new List<EquipamentoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaEquipamento", p);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;

        }
    }
}
