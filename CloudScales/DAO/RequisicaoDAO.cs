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
    public class RequisicaoDAO 
    {
        public async Task<Root> Requisicao()
    {
            //Requisição GET
            string url = "http://18.222.178.90:1026/v2/entities?id=urn:ngsi-ld:entity:004";
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
            Root resposta = JsonConvert.DeserializeObject<Root>(content);
            return resposta;

            /*
            //Salvando resposta em listas do modelo Root
            List<string> wA = new List<string>(); //valor do pesoA de TODAS AS ENTIDADES
            List<string> wB = new List<string>(); //valor do pesoA de TODAS AS ENTIDADES
            List<string> entityId = new List<string>(); //valor do id da entidade de TODAS AS ENTIDADES
            List<string> entityType = new List<string>(); //valor do tipo de TODAS AS ENTIDADES
            List<string> loadcellA_Id = new List<string>(); //valor do id da celula A de TODAS AS ENTIDADES
            List<string> loadcellB_Id = new List<string>(); //valor do id da celula A de TODAS AS ENTIDADES
            List<string> sinc = new List<string>(); //valor do sinc de TODAS AS ENTIDADES

            wA.AddRange(resposta.Select(m => m.PesoA.Value.ToString()));
            wB.AddRange(resposta.Select(m => m.PesoB.Value.ToString()));
            entityId.AddRange(resposta.Select(m => m.Id.ToString()));
            entityType.AddRange(resposta.Select(m => m.PesoA.Type.ToString()));
            loadcellA_Id.AddRange(resposta.Select(m => m.IdA.Value.ToString()));
            loadcellB_Id.AddRange(resposta.Select(m => m.IdB.Value.ToString()));
            if (sinc.Count != 0)
            {
                sinc.AddRange(resposta.Select(m => m.Sinc.Value.ToString()));
            }

            foreach (string resp in wA)
            {
                Console.WriteLine(resp);
            }
            */
        }
    }
}
