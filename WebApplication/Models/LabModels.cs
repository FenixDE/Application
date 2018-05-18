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
            if (response.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(response.Content);
                Lab labs = result.Data.ToObject<Lab>();
                return labs;
            }
            else
                return null;
        }

        /// <summary>
        /// Возвращает полный список факультетов
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Lab>> GetCollectionAsync()
        {
            var client = new RestClient("http://eljournal.ddns.net/api/LabWork");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            //request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(response.Content);
                //List<dynamic> faculties = result.Data as List<dynamic>;
                return result.Data.ToObject<List<Lab>>();
            }
            else
                return new List<Lab>();
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
            if (restResponse.IsSuccessful)
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
            string lab = JsonConvert.SerializeObject(this);
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/LabWork/{0}", ID));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", lab, ParameterType.RequestBody);
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
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/LabWork/{0}", ID));
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
                return true;
            else
                return false;
        }
    }

    public class LabPlan
    {
        public string ID { get; set; }
        public virtual Lab Work { get; set; }
        public string FlowSubjectId { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkID { get; set; }

        public static async Task<List<LabPlan>> GetCollectionAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new List<LabPlan>();

            var client = new RestClient(string.Format("http://eljournal.ddns.net/api/LabWork/plan/{0}", id));
            var request = new RestRequest(Method.GET);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            if (restResponse.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(restResponse.Content);
                List<LabPlan> planWorks = result.Data.ToObject<List<LabPlan>>();
                return planWorks;
            }
            else
                return new List<LabPlan>();
        }

        public async Task<bool> Push()
        {
            var client = new RestClient(string.Format("http://eljournal.ddns.net/api/LabWork/plan/{0}/{1}", 
                FlowSubjectId, WorkID));
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            return restResponse.IsSuccessful;
        }
    }
}