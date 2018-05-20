using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Models
{
    public class FlowSubject
    {
        public string ID { get; set; }
        public string FlowId { get; set; }
        public string TeacherId { get; set; }

        public string Teacher2Id { get; set; }
        public string Teacher3Id { get; set; }
        public string SubjectId { get; set; }
        public string SemesterId { get; set; }


        public static async Task<FlowSubject> GetInstanceAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var client = new RestClient(string.Format("http://eljournal.ddns.net/api/Subjects/flow/{0}", id));
            var request = new RestRequest(Method.GET);
            IRestResponse restResponse = client.Execute(request);
            if (restResponse.IsSuccessful)
            {
                Response result = JsonConvert.DeserializeObject<Response>(restResponse.Content);
                FlowSubject subject = result.Data.ToObject<FlowSubject>();
                return subject;
            }
            else
                return null;

        }
        //public static async Task<List<LabWorkPlan>> GetCollectionAsync(string )
        //{
        //    //if (string.IsNullOrEmpty(id))
        //    //    return new List<LabWorkPlan>();
        //    var client = new RestClient("http://eljournal.ddns.net/api/LabWork/plan");
        //    var request = new RestRequest(Method.GET);
        //    var cancellationTokenSource = new CancellationTokenSource();
        //    IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
        //    if (restResponse.IsSuccessful)
        //    {
        //        Response result = JsonConvert.DeserializeObject<Response>(restResponse.Content);
        //        List<LabWorkPlan> planWorks = result.Data.ToObject<List<LabWorkPlan>>();
        //        return planWorks;
        //    }
        //    else
        //        return new List<LabWorkPlan>();
        //}
    }
}