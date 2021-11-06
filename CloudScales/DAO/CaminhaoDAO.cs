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
    public class CaminhaoDAO : PadraoDAO<CaminhaoViewModel>
    {
        protected override SqlParameter[] CriaParametros(CaminhaoViewModel model)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("Id", model.Id),
                new SqlParameter("ClienteID", model.ClienteID),
                new SqlParameter("Placa", model.Placa),
                new SqlParameter("Carreta", model.Carreta)
            };
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
            Tabela = "Equipamento";
        }
    }
}
