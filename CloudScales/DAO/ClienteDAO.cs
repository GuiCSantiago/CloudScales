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
    public class ClienteDAO : PadraoDAO<ClienteViewModel>
    {
        protected override SqlParameter[] CriaParametros(ClienteViewModel model, string operacao)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("Id", model.Id),
                new SqlParameter("Nome", model.Nome),
                new SqlParameter("CNPJ", model.CNPJ),
                new SqlParameter("Email", model.Email),
                new SqlParameter("Senha", model.Senha)
            };
            return parametros;
        }

        protected override ClienteViewModel MontaModel(DataRow registro)
        {
            ClienteViewModel model = new ClienteViewModel();
            model.Id = Convert.ToInt32(registro["Id"]);
            model.Nome = registro["Nome"].ToString();
            model.CNPJ = Convert.ToInt32(registro["CNPJ"]);
            model.Email = registro["Email"].ToString();
            model.Senha = registro["Senha"].ToString();
            return model;
        }
        protected override void SetTabela()
        {
            Tabela = "Cliente";
        }

    }
}
