//using ElJournal.DBInteract;
//using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Models
{
    public class Flow
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string AltName { get; set; }
        public string FacultyId { get; set; }

        /// <summary>
        /// Возвращает поток по id
        /// </summary>
        /// <param name="id">id предмета</param>
        /// <returns></returns>
        public static async Task<Flow> GetInstanceAsync(string id)
        {            
                    return null;            
        }

        /// <summary>
        /// Возвращает полный список потоков
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Flow>> GetCollectionAsync()
        {
            return null;
        }


        /// <summary>
        /// Сохраняет текущий объект Flow в БД
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