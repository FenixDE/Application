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
    public class Subject
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DepartmentID { get; set; }
        public string Info { get; set; }
        public string Cypher { get; set; }


        public static async Task<dynamic> GetInstanceAsync(string id)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Subjects/{0}", id));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(response.Content);
                Subject subjects = result.Data.ToObject<Subject>();
                return subjects;
            }
            else
                return null;
        }

        
        public static async Task<List<Subject>> GetCollectionAsync()
        {
            var client = new RestClient("http://eljournal.ddns.net/api/Subjects");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            List<Subject> subjects = result.Data.ToObject <List<Subject>>();
            if (response.IsSuccessful)
                return subjects;
            else
                return new List<Subject>();
        }

        public async Task<List<FlowSubject>> GetForFlow()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Subjects/flow?subject={0}", ID));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(response.Content);
                List<FlowSubject> subjects = result.Data.ToObject<List<FlowSubject>>();
                return subjects;
            }
            else
                return new List<FlowSubject>();
        }

        public static async Task<bool> AddToFlow(FlowSubject flowSubject)
        {
            string fSubjString = JsonConvert.SerializeObject(flowSubject);
            var client = new RestClient("http://eljournal.ddns.net/api/Subjects/flow");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", fSubjString, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод           
            if (restResponse.IsSuccessful)
                return true;
            else
                return false;
        }

        public async Task<bool> Push()
        {
            string subject = JsonConvert.SerializeObject(this);
            var client = new RestClient("http://eljournal.ddns.net/api/Subjects");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", subject, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            if (restResponse.IsSuccessful)
                return true;
            else
                return false;
        }

        
        public async Task<bool> Update()
        {
            string subject = JsonConvert.SerializeObject(this);
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Subjects/{0}", ID));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", subject, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            if (restResponse.IsSuccessful)
                return true;
            else
                return false;
        }

        
        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Subjects/{0}", ID));
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
}