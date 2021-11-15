using CloudScales.Models.ViewModels;
using MVCJogos.DAO;
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

        public List<CaminhaoViewModel> ListaCaminhao()
        {
            List<CaminhaoViewModel> lista = new List<CaminhaoViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemCaminhao", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }

        public List<CaminhaoViewModel> FiltrarConteudo(CaminhaoViewModel filter)
        {
            var caminhoesFiltrados = new List<CaminhaoViewModel>();

            StringBuilder query = new StringBuilder();

            query.Append($@"
                            select 
                                 *
                            from
	                            dbo.Caminhao
                            where
                                 1=1 ");

            if (!string.IsNullOrEmpty(filter.Placa))
                query.Append($" and Placa like '%{filter.Placa}%' ");

            if (!string.IsNullOrEmpty(filter.Carreta))
                query.Append($" and Carreta like '%{filter.Carreta}%' ");

            using (SqlConnection conexao = ConexaoBD.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(query.ToString(), conexao))
                {
                    SqlDataReader dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        CaminhaoViewModel caminhao = new CaminhaoViewModel
                        {
                            Id = Convert.ToInt32(dataReader["ID"]),
                            ClienteID = Convert.ToInt32(dataReader["ClienteID"]),
                            Placa = Convert.ToString(dataReader["Placa"]),
                            Carreta = Convert.ToString(dataReader["Carreta"])
                        };

                        caminhoesFiltrados.Add(caminhao);
                    }

                }

                conexao.Close();
            }

            return caminhoesFiltrados;

        }
    }
}
