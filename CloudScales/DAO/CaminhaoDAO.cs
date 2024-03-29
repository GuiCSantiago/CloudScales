﻿using CloudScales.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScales.DAO
{
    public class CaminhaoDAO : PadraoDAO<CaminhaoViewModel>
    {
        protected override SqlParameter[] CriaParametros(CaminhaoViewModel model, string operacao)
        {
            SqlParameter[] parametros;
            if (operacao != "I")
            {
                parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("Id", model.Id);
                parametros[1] = new SqlParameter("ClienteID", model.ClienteID);
                parametros[2] = new SqlParameter("Placa", model.Placa);
                parametros[3] = new SqlParameter("Carreta", model.Carreta);
            }
            else
            {
                parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("ClienteID", model.ClienteID);
                parametros[1] = new SqlParameter("Placa", model.Placa);
                parametros[2] = new SqlParameter("Carreta", model.Carreta);
            }
            return parametros;
        }

        protected override CaminhaoViewModel MontaModel(DataRow registro)
        {
            CaminhaoViewModel model = new CaminhaoViewModel();
            model.Id = Convert.ToInt32(registro["Id"]);
            model.ClienteID = Convert.ToInt32(registro["ClienteID"]);
            model.Placa = registro["Placa"].ToString();
            model.Carreta = registro["Carreta"].ToString();
            return model;
        }
        protected override void SetTabela()
        {
            Tabela = "Caminhao";
        }

        public List<CaminhaoViewModel> ListaCaminhao(int id)
        {
            SqlParameter[] parametro = { new SqlParameter("id", id) };
            List<CaminhaoViewModel> lista = new List<CaminhaoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemCaminhao", parametro);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }
        
        public List<CaminhaoViewModel> ConsultaAvancadaCaminhao(string placa, string carreta, int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("placa", placa),
                new SqlParameter("carreta", carreta)
            };
            List<CaminhaoViewModel> lista = new List<CaminhaoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaCaminhao", p);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;

        }
    }
}
