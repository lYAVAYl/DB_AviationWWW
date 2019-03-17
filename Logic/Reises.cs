using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Reises
    {
        public Airports Airport_FlyOut { get; set; }
        public Airports Airport_FlyIn { get; set; }

        public DateTime Data_FlyOut { get; set; }
        public DateTime Data_FlyIn { get; set; }

        public string PlaneMark { get; set; }

        public Prices ReisCode { get; set; }



        public List<Reises> Reises_List = new List<Reises>();


        public void AddReise(Airports airports, Prices prices)
        {
            // Аэропорт вылета:    новый | существующий

            // Аэропорт вылета:    новый | существующий
            // Аэропорт прилёта:    новый | существующий

            // Дата и время вылета: уникальный
            // Дата и время вылета: уникальный

            // Марка самолёта: уникальный

            Console.WriteLine("---> Добавление Рейса <---\n\n\n\n\n\n\n");


            Console.Write("");








        }

        public void PrintReis()
        {
            





        }








    }
}
