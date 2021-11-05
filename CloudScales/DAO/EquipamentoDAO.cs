using CloudScales.Models.ViewModels;
using MVCJogos.DAO;
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
        protected override SqlParameter[] CriaParametros(EquipamentoViewModel model)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("CaminhaoID", model.Id),
                new SqlParameter("QtdBalanca", model.QtdBalanca)
            };
            return parametros;
        }

        protected override EquipamentoViewModel MontaModel(DataRow registro)
        {
            EquipamentoViewModel model = new EquipamentoViewModel();
            model.Id = Convert.ToInt32(registro["CaminhaoID"]);
            model.QtdBalanca = Convert.ToInt32(registro["QtdBalanca"]);
            return model;
        }
        protected override void SetTabela()
        {
            Tabela = "Equipamento";
        }
    }
}
