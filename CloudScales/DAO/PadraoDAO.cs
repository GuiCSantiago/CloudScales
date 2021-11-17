using CloudScales.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static RequisicaoViewModel;

namespace CloudScales.DAO
{
    public abstract class PadraoDAO<T> where T : PadraoViewModel
    {
        public PadraoDAO()
        {
            SetTabela();
        }

        protected string Tabela { get; set; }
        protected string NomeSpListagem { get; set; } = "spListagem";
        protected abstract SqlParameter[] CriaParametros(T model, string operacao);
        protected abstract T MontaModel(DataRow registro);
        protected abstract void SetTabela();

        public virtual void Insert(T model, string operacao)
        {
            HelperDAO.ExecutaProc("spInsert_" + Tabela, CriaParametros(model, operacao));
        }

        public virtual void Update(T model, string operacao)
        {
            HelperDAO.ExecutaProc("spUpdate_" + Tabela, CriaParametros(model, operacao));
        }

        public virtual void Delete(int id)
        {
            var p = new SqlParameter[]
            {
            new SqlParameter("id", id),
            new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutaProc("spDelete", p);
        }

        public virtual T Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public virtual int ProximoId()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0][0]);
        }

        public virtual List<T> Listagem(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }
    }
}