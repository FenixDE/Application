//using ElJournal.DBInteract;
//using NLog;
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
    public class Group
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CuratorId { get; set; }
        public string FacultyId { get; set; }
        public List<Semester> Semesters { get; set; }

        /// <summary>
        /// Возвращает группу по id
        /// </summary>
        /// <param name="id">id группы</param>
        /// <returns></returns>
        public static async Task<Group> GetInstanceAsync(string id)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Groups/{0}", id));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            Group groups = result.Data.ToObject<Group>();
            if (response.StatusCode == HttpStatusCode.OK)
                return groups;
            else
                return null;
        }

        /// <summary>
        /// Возвращает полный список групп
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Group>> GetCollectionAsync()
        {
            var client = new RestClient("http://eljournal.ddns.net/api/Groups");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            List<Group> groups = result.Data.ToObject <List<Group>>();
            if (response.StatusCode == HttpStatusCode.OK)
                return groups;
            else
                return new List<Group>();
        }


        public static async Task<List<Group>> GetGroupSemester(string semesterId)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Groups/BySemester/{0}",semesterId));
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Response result = JsonConvert.DeserializeObject<Response>(response.Content);
            List<Group> groups = result.Data.ToObject<List<Group>>();
            return groups;
        }

        /// <summary>
        /// Добавляет группу на указанный семестр
        /// </summary>
        /// <param name="semesterId">id семестра</param>
        /// <returns></returns>
        public async Task<bool> AddToSemester(string id, string semesterid)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Groups/{0}/{0}", id, semesterid));
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            //var cancellationTokenSource = new CancellationTokenSource();
            //IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод           
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Удаляет группу из указанного семестра (при наличии студентов в группе удаление невозможно)
        /// </summary>
        /// <param name="semesterId">id семестра</param>
        /// <returns></returns>
        public bool DeleteToSemester(string id, string semesterid)
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Groups/{0}/{0}", id, semesterid));
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Сохраняет текущий объект Group в БД
        /// </summary>
        /// <returns>True, если объект был добавлен в БД</returns>
        public async Task<bool> Push()
        {
            string group = JsonConvert.SerializeObject(this);
            var client = new RestClient("http://eljournal.ddns.net/api/Groups");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", group, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            //IRestResponse response = client.Execute(request);
            if (restResponse.StatusCode == HttpStatusCode.OK)
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
            string group = JsonConvert.SerializeObject(this);
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Groups/{0}", ID));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "38A1903A-622D-4201-BC6C-25E23D805771");
            request.AddParameter("undefined", group, ParameterType.RequestBody);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token); //ассинхронный метод
            return false;
        }

        /// <summary>
        /// Удаление текущего объекта из БД
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            var client = new RestClient(String.Format("http://eljournal.ddns.net/api/Groups/{0}", ID));
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