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
    public class Lab
    {
        public string ID { get; set; }
        public string Name { get; set; }        
        public string Advanced { get; set; }
        public string AuthorId { get; set; }

        /// <summary>
        /// Возвращает факультет по id
        /// </summary>
        /// <param name="id">id факультета</param>
        /// <returns></returns>
        public static async Task<dynamic> GetInstanceAsync(string id)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/LabWork/{0}", id));
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            Lab labs = result.Data.ToObject<Lab>();
            return labs;
        }

        /// <summary>
        /// Возвращает полный список факультетов
        /// </summary>
        /// <returns></returns>
        public static async Task<dynamic> GetCollectionAsync()
        {
            var client = new RestClient("http://eljournal.ddns.net/api/LabWork");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            //request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            //List<dynamic> faculties = result.Data as List<dynamic>;
            if (response.StatusCode == HttpStatusCode.OK)
                return result.Data;
            else
                return false;
        }

        /// <summary>
        /// Сохраняет текущий объект Faculty в БД
        /// </summary>
        /// <returns>True, если объект был добавлен в БД</returns>
        public async Task<bool> Push()
        {
            string lab = JsonConvert.SerializeObject(this);
            var client = new RestClient("http://eljournal.ddns.net/api/LabWork");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", lab, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            //IRestResponse response = client.Execute(request);
            return false;
        }

        /// <summary>
        /// Обновляет в БД выбранный объект (по ID)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Update()
        {
            string lab = JsonConvert.SerializeObject(this);
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/LabWork/{0}", ID));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", lab, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            //if (restResponse.StatusCode == HttpStatusCode.OK)
                return true;
            //else
            //    return false;
        }

        /// <summary>
        /// Удаление текущего объекта из БД
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/LabWork/{0}", ID));
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
    }
}