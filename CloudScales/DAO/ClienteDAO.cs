﻿using CloudScales.Models.ViewModels;
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
            SqlParameter[] parametros;
            if(operacao != "I")
            {
                parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("Id", model.Id);
                parametros[1] = new SqlParameter("Nome", model.Nome);
                parametros[2] = new SqlParameter("CNPJ", model.CNPJ);
                parametros[3] = new SqlParameter("Email", model.Email);
                parametros[4] = new SqlParameter("Senha", model.Senha);
            }
            else
            {
                parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("Nome", model.Nome);
                parametros[1] = new SqlParameter("CNPJ", model.CNPJ);
                parametros[2] = new SqlParameter("Email", model.Email);
                parametros[3] = new SqlParameter("Senha", model.Senha);
            }
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
