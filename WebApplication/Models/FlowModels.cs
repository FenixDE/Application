using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Threading;

namespace WebApplication.Models 
{
    public class Flow
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string AltName { get; set; }
        public string DepartmentId { get; set; }

        /// <summary>
        /// Возвращает поток по id
        /// </summary>
        /// <param name="id">id предмета</param>
        /// <returns></returns>        
        public static async Task<Flow> GetInstanceAsync(string id)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Flows/{0}", id));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            Flow flows = result.Data.ToObject<Flow>();
            if (response.IsSuccessful)
                return flows;
            else
                return null;
        }

        /// <summary>
        /// Возвращает полный список потоков
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Flow>> GetCollectionAsync()
        {
            var client = new RestClient("http://eljournal.ddns.net/api/Flows");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            List<Flow> flows = result.Data.ToObject<List<Flow>>();
            if (response.IsSuccessful)
                return flows;
            else
                return new List<Flow>();
        }


        /// <summary>
        /// Сохраняет текущий объект Flow в БД
        /// </summary>
        /// <returns>True, если объект был добавлен в БД</returns>
        public async Task<bool> Push()
        {
            string subject = JsonConvert.SerializeObject(this);
            var client = new RestClient("http://eljournal.ddns.net/api/Flows");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", subject, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод                                                                                                                

            //if (restResponse.StatusCode == HttpStatusCode.OK)
            if(restResponse.IsSuccessful)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Обновляет в БД выбранный объект (по ID)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Update()
        {
            string flow = JsonConvert.SerializeObject(this);
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Flows/{0}", ID));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", flow, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            if (restResponse.IsSuccessful)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Удаление текущего объекта из БД
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Flows/{0}", ID));
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
                return true;
            else
                return false;
        }
    }
}