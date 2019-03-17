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


            Console.Write("Аэропорт вылета: ");
            Console.ReadLine();
            airports.Airport_Adding();
            Reises_List.Add(new Reises { Airport_FlyOut = airports.Airport_List.LastOrDefault() });

            Console.WriteLine("Аэропорт прилёта: ");
            Console.ReadLine();
            airports.Airport_Adding();
            Reises_List.Add(new Reises { Airport_FlyIn = airports.Airport_List.LastOrDefault() });

            Console.WriteLine("Код аэропорта: ");
            Console.ReadLine();
            prices.AddPriceForReis();
            Reises_List.Add(new Reises { ReisCode = prices.Prices_List.Last() });
        }


        public void PrintReis()
        {
            Console.WriteLine(Reises_List[0].Airport_FlyIn.Airport_Code);
            Console.WriteLine(Reises_List[0].Airport_FlyIn.Airport_City);
            Console.WriteLine(Reises_List[0].Airport_FlyIn.Airport_Name);

        }








    }
}
