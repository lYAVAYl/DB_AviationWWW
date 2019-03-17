using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Aviation_BisLog.Model
{
    [Serializable]
    public class Airports
    {
        /// <summary>
        /// Название аэропорта
        /// </summary>
        public string AirportName { get; set; }

        /// <summary>
        /// Город аэропорта
        /// </summary>
        public string AirportCity { get; set; }

        /// <summary>
        /// Код аэропорта
        /// </summary>
        public string AirportCode { get; set; }

        /// <summary>
        /// Ввод названия аэропорта
        /// </summary>
        /// <param name="name"> Название аэропорта </param>
        public Airports(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Поле названия аэропорта не заполнено");
            }
        }






    }
}
