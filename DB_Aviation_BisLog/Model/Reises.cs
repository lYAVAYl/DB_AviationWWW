using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Aviation_BisLog.Model
{
    [Serializable]
    public class Reises
    {
        #region Свойства
        /// <summary>
        /// Аэропорт вылета
        /// </summary>
        public Airports FlyOut_Airport { get; set; }

        /// <summary>
        /// Аэропорт прилёта
        /// </summary>
        public Airports FlyIn_Airport { get; set; }

        /// <summary>
        /// Дата вылета
        /// </summary>
        public DateTime FlyOut_dateTime { get; set; }

        /// <summary>
        /// Дата прилёта
        /// </summary>
        public DateTime FlyIn_dateTime { get; set; }

        /// <summary>
        /// Марка самолёта
        /// </summary>
        public string PlaneMark { get; set; }

        /// <summary>
        /// Код рейса
        /// </summary>
        public int ReisCode { get; set; }
        #endregion

        #region /// <summary> ...
        /// <summary>
        /// конструктор проверки на корректность
        /// </summary>
        /// <param name="flyout_airp"> аэропорт вылета </param>
        /// <param name="flyin_airp"> аэропорт прилёта </param>
        /// <param name="flyout_dt"> дата и время вылета </param>
        /// <param name="flyin_dt"> дата и время прилёта </param>
        /// <param name="planemark"> марка самолёта </param>
        /// <param name="reisecode"> код рейса </param>
        #endregion
        public Reises(Airports flyout_airp,
                      Airports flyin_airp, 
                      DateTime flyout_dt, 
                      DateTime flyin_dt, 
                      string planemark,
                      int reisecode)
        {
            #region Проверка -> ещё доделать
            if (flyout_airp==null)
            {
                throw new ArgumentNullException("Название аэропорта вылета не может быть пустым!", nameof(flyout_airp));
            }
            if (flyin_airp==null)
            {
                throw new ArgumentNullException("Название аэропорта прилёта не может быть пустым!", nameof(flyin_airp));
            }
            if (flyout_dt<DateTime.Parse("01.01.2010"))
            {
                throw new ArgumentNullException("Дата вылета  не может быть больше 10 лет назад!");
            }
            if (flyin_dt < DateTime.Parse("01.01.2010"))
            {
                throw new ArgumentNullException("Дата прилёта  не может быть больше 10 лет назад!");
            }
            if (flyin_dt>flyout_dt || flyin_dt == flyout_dt)
            if (string.IsNullOrWhiteSpace(planemark))
            {
                    throw new ArgumentNullException("Ошибка времени!");
            }
            if (reisecode < 0)
            {
                throw new ArgumentException("Код рейса не модет быть меньше 0!");
            }
            #endregion

            FlyOut_Airport = flyout_airp;
            FlyIn_Airport = flyin_airp;
       
            FlyOut_dateTime = flyout_dt;
            FlyIn_dateTime = flyin_dt;

            PlaneMark = planemark;

            ReisCode = reisecode;
        }





    }
}
