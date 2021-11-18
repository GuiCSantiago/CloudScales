using CloudScales.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.DAO
{
    public class CelulaDAO : PadraoDAO<CelulaViewModel>
    {
        protected override SqlParameter[] CriaParametros(CelulaViewModel model, string operacao)
        {
            SqlParameter[] parametros;
            if (operacao != "I")
            {
                parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("Id", model.Id);
                parametros[1] = new SqlParameter("EquipamentoID", model.EquipamentoID);
                parametros[2] = new SqlParameter("Peso", model.Peso);
                parametros[3] = new SqlParameter("Posicao", model.Posicao);
            }
            else
            {
                parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("EquipamentoID", model.EquipamentoID);
                parametros[1] = new SqlParameter("Peso", model.Peso);
                parametros[2] = new SqlParameter("Posicao", model.Posicao);
            }
            return parametros;
        }

        protected override CelulaViewModel MontaModel(DataRow registro)
        {
            CelulaViewModel model = new CelulaViewModel();
            model.Id = Convert.ToInt32(registro["Id"]);
            model.EquipamentoID = Convert.ToInt32(registro["EquipamentoID"]);
            model.Peso = Convert.ToDouble(registro["Peso"]);
            model.Posicao = Convert.ToInt32(registro["Posicao"]);
            return model;
        }
        protected override void SetTabela()
        {
            Tabela = "Celula";
        }

        public List<CelulaViewModel> ConsultaPorEquipamento(int equipamentoID)
        {
            var p = new SqlParameter[]
            {
            new SqlParameter("id", equipamentoID),
            };
            List<CelulaViewModel> lista = new List<CelulaViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaCelulaPorEquipamento", p);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }

        public List<CelulaViewModel> ConsultaAvancadaCelula(string equipamentoID, string peso, string posicao, int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("equipamentoID", equipamentoID),
                new SqlParameter("peso", peso),
                new SqlParameter("posicao", posicao)
            };
            List<CelulaViewModel> lista = new List<CelulaViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaCelula", p);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }
    }
}
