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
    public class Student
    {
        public string ID { get; set; }
        public string PersonId { get; set; }
        public string GroupId { get; set; }
        public string SemesterId { get; set; }


        public static async Task<Student> GetInstanceAsync(string id)
        {
            var client = new RestClient(string.Format("http://eljournal.ddns.net/api/Students/{0}", id));
            var request = new RestRequest(Method.GET);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод                                                                                                                
            if (restResponse.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(restResponse.Content);
                Student student = result.Data.ToObject<Student>();
                return student;
            }
            else
                return null;
        }
               
        public static async Task<List<Student>> GetCollectionAsync(string groupId, string semesterid)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Students/group/{0}/{1}", semesterid, groupId));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(response.Content);
                List<Student> students = result.Data.ToObject<List<Student>>();
                return students;
            }
            else
                return new List<Student>();
        }


        public class StudentFlowSubject
        {
            public string ID { get; set; }
            public string StudentId { get; set; }
            public string FlowSubjectId { get; set; }

            public static async Task<List<StudentFlowSubject>> GetCollectionAsync(string flowSubjectId)
            {
                var client = new RestClient(string.Format("http://eljournal.ddns.net/api/Students/flow/{0}", flowSubjectId));
                var request = new RestRequest(Method.GET);
                request.AddHeader("Postman-Token", "b27d2b8f-718a-4d65-9f24-c1472244004d");
                request.AddHeader("Cache-Control", "no-cache");
                var cancellationTokenSource = new CancellationTokenSource();
                IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                if (restResponse.IsSuccessful)
                {
                    Response result = JsonConvert.DeserializeObject<Response>(restResponse.Content);
                    List<StudentFlowSubject> students = result.Data.ToObject<List<StudentFlowSubject>>();
                    return students;
                }
                else
                    return new List<StudentFlowSubject>();
            }

            public async Task<bool> Add()
            {
                string studentFlow = JsonConvert.SerializeObject(this);
                var client = new RestClient("http://eljournal.ddns.net/api/Students/flow");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
                request.AddParameter("undefined", studentFlow, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var cancellationTokenSource = new CancellationTokenSource();
                IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                return restResponse.IsSuccessful;
            }

            public async Task<bool> Delete()
            {
                var client = new RestClient(string.Format("http://eljournal.ddns.net/api/Students/flow/{0}", ID));
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
                var cancellationTokenSource = new CancellationTokenSource();
                IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                return restResponse.IsSuccessful;
            }
        }



        public async Task<bool> Push()
        {
            string subject = JsonConvert.SerializeObject(this);
            var client = new RestClient("http://eljournal.ddns.net/api/Students");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", subject, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод                                                                                                                
            return restResponse.IsSuccessful;
        }
                

        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Students/{0}", ID));
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