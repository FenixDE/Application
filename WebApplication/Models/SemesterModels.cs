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
    public class Semester
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public DateTime? SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }


        /// <summary>
        /// Возвращает группу по id
        /// </summary>
        /// <param name="id">id группы</param>
        /// <returns></returns>        
        public static async Task<dynamic> GetInstanceAsync(string id)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Semester/{0}", id));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            Semester semesters = result.Data.ToObject<Semester>();
            return semesters;
        }
        public static async Task<dynamic> GetCollectionAsync()
        {
            var client = new RestClient("http://eljournal.ddns.net/api/Semester");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            //return result.Data;
            List<Semester> semesters = result.Data.ToObject<List<Semester>>();
            if (response.StatusCode == HttpStatusCode.OK)
                return semesters;
            else
                return new List<Semester>();
        }
        public async Task<bool> Push()
        {
            string semester = JsonConvert.SerializeObject(this);
            var client = new RestClient("http://eljournal.ddns.net/api/Semester");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", semester, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод //IRestResponse response = client.Execute(request);            
            return false;
        }
        public async Task<bool> Update()
        {
            string semester = JsonConvert.SerializeObject(this);
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Semester/{0}", ID));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", semester, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            return false;
        }
        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Semester/{0}", ID));
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