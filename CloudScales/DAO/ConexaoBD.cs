using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudScales.DAO
{
    public static class ConexaoBD
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=DESKTOP-GUILHER\\SQLEXPRESS; Database=CloudScalesDB; user id=sa; password=123456";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
