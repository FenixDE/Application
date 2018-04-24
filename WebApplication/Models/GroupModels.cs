//using ElJournal.DBInteract;
//using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElJournal.Models
{
    public class Group
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CuratorId { get; set; }
        public string FacultyId { get; set; }
        //public List<Semester> Semesters { get; set; }

        /// <summary>
        /// Возвращает группу по id
        /// </summary>
        /// <param name="id">id группы</param>
        /// <returns></returns>
        public static async Task<Group> GetInstanceAsync(string id)
        {
            return null;
        }

        /// <summary>
        /// Возвращает полный список групп
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Group>> GetCollectionAsync()
        {
            return null;
        }
        

        /// <summary>
        /// Добавляет группу на указанный семестр
        /// </summary>
        /// <param name="semesterId">id семестра</param>
        /// <returns></returns>
        public async Task<bool> AddToSemester(string semesterId)
        {
            return false;
        }

        /// <summary>
        /// Удаляет группу из указанного семестра (при наличии студентов в группе удаление невозможно)
        /// </summary>
        /// <param name="semesterId">id семестра</param>
        /// <returns></returns>
        public bool DeleteToSemester(string semesterId)
        {
            return false;
        }

        /// <summary>
        /// Сохраняет текущий объект Group в БД
        /// </summary>
        /// <returns>True, если объект был добавлен в БД</returns>
        public async Task<bool> Push()
        {
            return false;
        }

        /// <summary>
        /// Обновляет в БД выбранный объект (по ID)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Update()
        {
            return false;
        }

        /// <summary>
        /// Удаление текущего объекта из БД
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            return false;
        }
    }
}