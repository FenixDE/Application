﻿using Newtonsoft.Json;
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

               
        public static async Task<List<Student>> GetCollectionAsync(string id, string semesterid)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Students/group/{0}/{0}", id, semesterid));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            List<Student> students = result.Data.ToObject<List<Student>>();
            if (response.StatusCode == HttpStatusCode.OK)
                return students;
            else
                return new List<Student>();
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
            return false;
        }
                

        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Students/{0}", ID));
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