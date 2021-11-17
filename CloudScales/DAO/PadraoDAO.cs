using CloudScales.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCJogos.DAO
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

        public virtual List<T> Requisicao()
        { 
            try
            {
                //Requisição GET
                string url = "http://18.222.178.90:1026/v2/entities";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("fiware-service", "helixiot");
                client.DefaultRequestHeaders.Add("fiware-servicepath", "/");
                
                //Tratamento da resposta
                var response = await client.GetAsync(client.BaseAddress);
                var status = response.StatusCode.GetHashCode();
                Console.WriteLine(response.StatusCode.GetTypeCode());
                Console.WriteLine(status);
                string content = await response.Content.ReadAsStringAsync();
                List<Root> resposta = JsonConvert.DeserializeObject<List<Root>>(content);

                //Salvando resposta em listas do modelo Root
                List<string> wA = new List<string>(); //valor do pesoA de TODAS AS ENTIDADES
                List<string> wB = new List<string>(); //valor do pesoA de TODAS AS ENTIDADES
                List<string> entityId = new List<string>(); //valor do id da entidade de TODAS AS ENTIDADES
                List<string> entityType = new List<string>(); //valor do tipo de TODAS AS ENTIDADES
                List<string> loadcellA_Id = new List<string>(); //valor do id da celula A de TODAS AS ENTIDADES
                List<string> loadcellB_Id = new List<string>(); //valor do id da celula A de TODAS AS ENTIDADES
                List<string> sinc = new List<string>(); //valor do sinc de TODAS AS ENTIDADES


                wA.AddRange(resposta.Select(m => m.pesoA.value.ToString()));
                wB.AddRange(resposta.Select(m => m.pesoB.value.ToString()));
                entityId.AddRange(resposta.Select(m => m.id.ToString()));
                entityType.AddRange(resposta.Select(m => m.pesoA.type.ToString()));
                loadcellA_Id.AddRange(resposta.Select(m => m.idA.value.ToString()));
                loadcellB_Id.AddRange(resposta.Select(m => m.idB.value.ToString()));
                if(sinc.Count != 0) { 
                sinc.AddRange(resposta.Select(m => m.sinc.value.ToString())); }


                foreach (string resp in wA){
                    Console.WriteLine(resp);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Consulta concluída");
                Console.ReadKey();
            }
        }
    }
}