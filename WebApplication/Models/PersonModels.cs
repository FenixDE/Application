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
    public class Person
    {
        public string ID { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Name { get; set; }
        public string Student_id { get; set; }
        public string Passport_id { get; set; }
        public string Secret { get; set; }
        public string Info { get; set; }
        public string RoleId { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Surname, Name, Patronymic);
        }

        public static async Task<Person> GetInstanceAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
                return null;

            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/People/{0}", id));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            Person people = result.Data.ToObject<Person>();//TODO: здесь необработанная ошибка
            return people;
        }

        
        public static async Task<List<Person>> GetCollectionAsync()
        {
            var client = new RestClient("http://eljournal.ddns.net/api/People");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            List<Person> people = result.Data.ToObject<List<Person>>();
            if (response.StatusCode == HttpStatusCode.OK)
                return people;
            else
                return new List<Person>();
        }


        
        public async Task<bool> Push()
        {
            string subject = JsonConvert.SerializeObject(this);
            var client = new RestClient("http://eljournal.ddns.net/api/People");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", subject, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод                                                                                                                
            return false;
        }

        
        public async Task<bool> Update()
        {
            string person = JsonConvert.SerializeObject(this);
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/People/{0}", ID));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", person, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            if (restResponse.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        
        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/People/{0}", ID));
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