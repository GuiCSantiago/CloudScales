using CloudScales.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudScales.DAO
{
    public class RequisicaoDAO 
    {
        public async Task<RequisicaoViewModel.Root> Requisicao()
        {
            //Requisição GET
            string url = "http://18.222.178.90:1026/v2/entities?id=urn:ngsi-ld:entity:1";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("fiware-service", "helixiot");
            client.DefaultRequestHeaders.Add("fiware-servicepath", "/");

            //Tratamento da resposta
            var response = await client.GetAsync(client.BaseAddress);
            var status = response.StatusCode.GetHashCode();
            //Console.WriteLine(response.StatusCode.GetTypeCode());
            //Console.WriteLine(status);
            string content = await response.Content.ReadAsStringAsync();
            List<RequisicaoViewModel.Root> resposta = JsonConvert.DeserializeObject<List<RequisicaoViewModel.Root>>(content);
            //Console.WriteLine(resposta);
            return resposta.First();
        }

        public async Task<bool> CriarEntidade(EquipamentoViewModel equipamento) //importa somente equipamentos com duas celulas
        {
            CelulaDAO celulaDAO = new CelulaDAO();
            try
            {
                List<CelulaViewModel> celulas = celulaDAO.ConsultaPorEquipamento(equipamento.Id); // retorna células por ordem de posição

                RequisicaoViewModel.Root root = new RequisicaoViewModel.Root();
                RequisicaoViewModel.Metadata metadata = new RequisicaoViewModel.Metadata();

                root.AtrasPesoMaior = new RequisicaoViewModel.AtrasPesoMaior() { type = "Boolean", value = false ,metadata = metadata }; 
                root.idA = new RequisicaoViewModel.IdA() { type = "Number", metadata = metadata };
                root.idB = new RequisicaoViewModel.IdB() { type = "Number", metadata = metadata }; 
                root.pesoA = new RequisicaoViewModel.PesoA() { type = "Number", metadata = metadata };
                root.pesoB = new RequisicaoViewModel.PesoB() { type = "Number", metadata = metadata };
                root.sinc = new RequisicaoViewModel.Sinc() { type = "Number", metadata = metadata };

                root.id = "urn:ngsi-ld:entity:" + equipamento.Id;

                root.type = "iot";

                root.idA.value = celulas[0].Id;

                root.idB.value = celulas[1].Id;

                string url = "http://18.222.178.90:1026/v2/entities";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("fiware-service", "helixiot");
                client.DefaultRequestHeaders.Add("fiware-servicepath", "/");

                string json = JsonConvert.SerializeObject(root);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var status = response.StatusCode.GetHashCode();
                if (status.Equals(201))
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);         
            }
            return false;
        }
    }
}
