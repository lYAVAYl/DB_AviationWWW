using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Taiwan Taoyuan International (Chiang Kai Shek International)
// Piper PA-31 Navajo Chieftain - Navajo Chieftain
namespace Logic
{
    public class Reises
    {
        public Airports Airport_FlyOut { get; set; }
        public Airports Airport_FlyIn { get; set; }

        public DateTime Data_FlyOut;
        public DateTime Data_FlyIn;

        public string PlaneMark = "";

        public Prices P_ReisCode { get; set; }



        public List<Reises> Reises_List = new List<Reises>();


        // =============================================================================

        // Добавление рейса
        #region AddReise
        public void AddReise(Airports airports, Prices prices)
        {
            // Аэропорт вылета:    новый | существующий

            // Аэропорт вылета:    новый | существующий
            // Аэропорт прилёта:    новый | существующий

            // Дата и время вылета: уникальный
            // Дата и время вылета: уникальный

            // Марка самолёта: уникальный

            int FlyIn_ind=0, FlyOut_ind=0;
            bool isNewAirportFlyOut = false;
            bool isNewAirportFlyIn = false;

            DateTime FlyOut_data = new DateTime();
            DateTime FlyIn_data = new DateTime();

            string planemark="";
            bool rightData = false;
            bool allright = false, exit = false;



            while (!allright)
            {
                #region Аэропорт вылета
                while (!exit)
                {

                    if (NewOrCreated("---> Добавление Рейса <---\n\n\n\n\n\n\n\nАэропорт вылета: ", ref exit))
                    {
                        airports.Airport_Adding(); // предпоследний в списке (predposl)
                        FlyOut_ind = airports.Airport_List.Count - 1;
                        isNewAirportFlyOut = true;
                    }
                    else
                    {
                        if (airports.Airport_List.Count >= 1)
                        {
                            FlyOut_ind = airports.ChooseCreatedAirport();
                            if (FlyOut_ind >= 0)
                                exit = true;
                        }
                        else
                        {
                            Console.WriteLine("\nСписок аэропортов не достаточно большой. Введите хотя бы 2 аэропорта.\n" +
                                              "Нажмите любую клавишу, чтобы продолжить...");
                            exit = false;
                            Console.ReadKey(true);
                        }
                        
                            
                    }

                }
                exit = false;
                #endregion

                #region Аэропорт прилёта
                while (!exit)
                {
                    
                    if (NewOrCreated("---> Добавление Рейса <---\n\n" +
                                     "Аэропорт вылета: " + airports.Airport_List[FlyOut_ind].Airport_Name + "\n\n\n\n\n" +
                                     "Аэропорт прилёта: ", ref exit))
                    {
                        airports.Airport_Adding(); // предпоследний в списке (predposl)
                        FlyIn_ind = airports.Airport_List.Count - 1;
                        isNewAirportFlyIn = true;

                    }
                    else
                    {
                        if (airports.Airport_List.Count >= 2)
                        {
                            FlyIn_ind = airports.ChooseCreatedAirport(FlyOut_ind);
                            if (FlyIn_ind >= 0 && FlyIn_ind!=FlyOut_ind)
                                exit = true;
                        }
                        else
                        {
                            Console.WriteLine("\nСписок аэропортов не достаточно большой. Введите хотя бы 2 аэропорта.\n" +
                                              "Нажмите любую клавишу, чтобы продолжить...");
                            exit = false;
                            Console.ReadKey(true);
                        }

                    }

                }
                exit = false;
                #endregion



                #region Код рейса + цена + места
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List[FlyIn_ind].Airport_Name + "\n\n\n\n");

                    Console.Write("Ввод кода рейса...");
                    var b = Console.ReadKey(true).Key;
                    if (b != ConsoleKey.Escape)
                    {
                        Console.Clear();
                        prices.AddPriceForReis(); // последний в списке
                        exit = true;
                    }
                    else
                        return;
                    
                }
                exit = false;
                #endregion



                #region Дата вылета
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List[FlyIn_ind].Airport_Name + "\n" +
                                      "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n\n\n");
                    try
                    {
                        Console.WriteLine("Дата вылета.");
                        exit = isCorrect_Date(out FlyOut_data);

                    }
                    catch
                    {
                        // вывод ошибки включён в метод isCorrect_Date()
                    }
                    
                }
                exit = false;
                #endregion

                #region Дата прилёта
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List[FlyIn_ind].Airport_Name + "\n" +
                                      "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n" +
                                      $"Дата вылета: {FlyOut_data.Day:00}.{FlyOut_data.Month:00}.{FlyOut_data.Year}   {FlyOut_data.Hour:00}:{FlyOut_data.Minute:00}\n\n");


                    try
                    {
                        Console.WriteLine("Дата прилёта.");
                        exit = isCorrect_Date(out FlyIn_data) && isCorrect_Difference(FlyOut_data, FlyIn_data);
                        
                    }
                    catch
                    {

                        Console.WriteLine("\n\nДанные введены не по образцу. Повторите попытку.\n" +
                                          "Нажмите любую клавишу, чтобы продолжить...");
                        Console.ReadKey(true);

                    }

                }
                exit = false;
                #endregion



                #region Марка самолёта
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List[FlyIn_ind].Airport_Name + "\n" +
                                      "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n" +
                                      $"Дата вылета: {FlyOut_data.Day:00}.{FlyOut_data.Month:00}.{FlyOut_data.Year}   {FlyOut_data.Hour:00}:{FlyOut_data.Minute:00}\n" +
                                      $"Дата прилёта: {FlyIn_data.Day:00}.{FlyIn_data.Month:00}.{FlyIn_data.Year}   {FlyIn_data.Hour:00}:{FlyIn_data.Minute:00}\n");

                    Console.Write("Марка самолёта: ");

                    planemark = Console.ReadLine();
                    planemark = planemark.Trim();
                    if (planemark.isCorrectPlaneMark())
                    {
                        Console.Clear();

                        exit = exit.AreUSure("---> Добавление Рейса <---\n\n" +
                                                     "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 2].Airport_Name + "\n" +
                                                     "Аэропорт прилёта: " + airports.Airport_List.Last().Airport_Name + "\n" +
                                                     "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n" +
                                                     $"Дата вылета: {FlyOut_data.Day}.{FlyOut_data.Month}.{FlyOut_data.Year}   {FlyOut_data.Hour}:{FlyOut_data.Minute}\n" +
                                                     $"Дата прилёта: {FlyIn_data.Day}.{FlyIn_data.Month}.{FlyIn_data.Year}   {FlyIn_data.Hour}:{FlyIn_data.Minute}\n" +
                                                     "\nМарка самолёта: " + planemark + "\n\n", "Вы уверены, что верно ввели марку самолёта?");
                    }
                                   
                }
                exit = false;
                #endregion


                //-----------------------------------------------------------------------------------------------------------------------------------------------

                #region Подтверждение ввода
               
                Console.Clear();
                Console.WriteLine();
                allright = allright.AreUSure("---> Добавление Рейса <---\n\n" +
                                             "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 2].Airport_Name + "\n" +
                                             "Аэропорт прилёта: " + airports.Airport_List.Last().Airport_Name + "\n" +
                                             "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n" +
                                             $"Дата вылета: {FlyOut_data.Day}.{FlyOut_data.Month}.{FlyOut_data.Year}   {FlyOut_data.Hour}:{FlyOut_data.Minute}\n" +
                                             $"Дата прилёта: {FlyIn_data.Day}.{FlyIn_data.Month}.{FlyIn_data.Year}   {FlyIn_data.Hour}:{FlyIn_data.Minute}\n" +
                                             "Марка самолёта: " + planemark + "\n\n");
                if (!allright)
                {
                    if (isNewAirportFlyOut && isNewAirportFlyIn)
                    {
                        airports.Airport_List.RemoveAt(airports.Airport_List.Count - 1);
                        airports.Airport_List.RemoveAt(airports.Airport_List.Count - 1);
                    }
                    else if (isNewAirportFlyOut || isNewAirportFlyIn)
                    {
                        airports.Airport_List.RemoveAt(airports.Airport_List.Count - 1);
                    }
                                       
                    prices.Prices_List.RemoveAt(prices.Prices_List.Count - 1);
                }
            }
            exit = false;
            #endregion

            // добавление рейса
            Reises_List.Add(new Reises { Airport_FlyOut = airports.Airport_List[FlyOut_ind]
                                        ,Airport_FlyIn = airports.Airport_List[FlyIn_ind]
                                        ,P_ReisCode = prices.Prices_List.Last()
                                        ,Data_FlyOut = FlyOut_data
                                        ,Data_FlyIn = FlyIn_data
                                        ,PlaneMark = planemark});            

        }
        #endregion




        #region NewOrCreated airport
        // Добавить новый аэропорт или ужу созданный?
        /// <summary>
        /// Новый аэропорт или уже созданный?
        /// </summary>
        /// <param name="info">верхняя строка</param>
        /// <param name="smallExit">условие выхода (что-то выбрано)</param>
        /// <returns></returns>
        private static bool NewOrCreated(string info, ref bool smallExit)
        {
            // выбор (от него зависит, что выведет)
            byte myChoise = 1;
            while (true) // цикл отображения ДА / НЕТ
            {
                Console.Clear();
                Console.Write(info);
                switch (myChoise) // что вывести в зависимости от значения myChoise
                {
                    #region Добавить новый
                    case 1: //------------------------------------------------ выделяем вариант ДА
                        Console.Write("   ");
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(" Добавить Новый ");
                        Console.ResetColor();
                        Console.WriteLine(" |  Выбрать из существующих"); //---------------------- всё покрасилось красиво

                        var button = Console.ReadKey(true).Key; // чтение клавиши и проверка на корректность
                        while (button != ConsoleKey.Enter && button != ConsoleKey.RightArrow && button != ConsoleKey.LeftArrow)
                        {
                            button = Console.ReadKey(true).Key; // изменение если неправильная клавиша
                        }

                        if (button == ConsoleKey.RightArrow || button == ConsoleKey.LeftArrow) // если <-|-> то
                        {
                            myChoise = 2; // выделяем вариант НЕТ
                        }
                        else if (button == ConsoleKey.Enter)
                        {
                            smallExit = true; // выход из 'малого' цикла
                            return true;
                        }
                        
                        break;
                    #endregion

                    #region Выбрать из существующих
                    case 2:
                        Console.Write("    Добавить новый  | ");//------------------------ выделяем вариант НЕТ
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" Выбрать из существующих ");
                        Console.ResetColor();//------------------------------- всё покрасилось красиво

                        button = Console.ReadKey(true).Key; // чтение клавиши + проверка
                        while (button != ConsoleKey.Enter && button != ConsoleKey.RightArrow && button != ConsoleKey.LeftArrow)
                        {
                            button = Console.ReadKey(true).Key; // изменение если неправильная клавиша
                        }

                        if (button == ConsoleKey.RightArrow || button == ConsoleKey.LeftArrow) // если <-|->, то 
                        {
                            myChoise = 1; // переходим на вариант ДА
                        }
                        else if (button == ConsoleKey.Enter)
                        {
                            smallExit = false; // выход из 'малого' цикла
                            return false;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }

            }

        }
        #endregion

        // Проверка даты на корректность (+- 2 года от текущей даты)
        #region isCorrect_Date
        private static bool isCorrect_Date(out DateTime inputDateTime)
        {
            try
            {
                Console.Write("Введите дату по образцу <День.Месяц.Год Час:Минута>: ");
                string dateStr = Console.ReadLine();
                inputDateTime = DateTime.ParseExact(dateStr, "dd'.'MM'.'yyyy' 'HH':'mm", CultureInfo.CurrentCulture);

                if ((inputDateTime.Year - DateTime.Now.Year) >= -2 && (inputDateTime.Year - DateTime.Now.Year) <= 2)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nГод указан некорректно. Повторите попытку.");
                    Console.ReadKey(true);
                    return false;
                }


            }
            catch
            {
                Console.WriteLine("\n\nДанные введены не по образцу. Повторите попытку.\n" +
                                          "Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey(true);
                inputDateTime = DateTime.MinValue;
                return false;
            }
            


        }
        #endregion


        // разница во времени между датой вылета и прилёта (макс. вермя полёта - 18 часов)
        #region isCorrect Difference between FlyOut & FlyIn dates
        private static bool isCorrect_Difference(DateTime FlyOut_date, DateTime FlyIn_date)
        {
            if (FlyIn_date < FlyOut_date) // дата прилёта раньше даты вылета
            {
                Console.WriteLine("\nДата прилёта не может быть раньше даты вылета. Заполните поле снова.\n" +
                                  "Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else // дата прилёта позже даты вылета
            {
                var DifferenceInTime = (FlyIn_date - FlyOut_date).TotalHours; // разница между вылетом и прилётом

                int hour = (int)DifferenceInTime; // разница в часах

                if (hour > 0 && hour <= 18) // время полёта 1-18 часов (верно)
                    return true;
                else if (hour < 1) // время полёта <1 часа
                {
                    Console.WriteLine("\nВремя полёта не может быть менее 1 часа и более 18 часов. \n" +
                                      "Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey(true);

                    return false;
                }
                else if (FlyIn_date.Hour <= FlyOut_date.Hour ) // время прилёта 
                {
                     Console.WriteLine("\nВремя прилёта не может быть раньше времени вылета. Заполните поле снова.\n" +
                                       "Нажмите любую клавишу, чтобы продолжить...");
                     Console.ReadKey();
                     return false;
                }
                else
                {
                    
                    Console.WriteLine("\nВремя полёта слишком большое \n" +
                                      "Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey(true);

                    return false;
                }

            }
            

        }
        #endregion

        // =============================================================================




        // =============================================================================
        // Вывод таблицы
        #region PrintReises
        public void PrintReises(Airports airports, Prices prices)
        {
            bool exit = false;
            int start_point = 0;
            int end_point = 2;

            int line = 0;            
            int column = 0;


            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                                  "║                                                               Таблица Рейсов                                                              ║\n" +
                                  "║                                                                                                                                           ║\n" +
                                  "╠═══════╦═══════════════════════════════════╦═══════════════════════════════════╦══════════════╦══════════════╦═════════════════════════════╣\n" +
                                  "║--код--║--        Аэропорт вылета        --║--        Аэропорт прилёта       --║-Дата и время-║-Дата и время-║--      Марка самолёта     --║\n" +
                                  "║-рейса-║                                   ║                                   ║    вылета    ║    вылета    ║                             ║");


                int i = start_point;
                while (((i + 1) % end_point > 0) && (i < Reises_List.Count) )
                {
                    ReisTablBody(Reises_List[i].P_ReisCode,
                                 Reises_List[i].Airport_FlyOut,
                                 Reises_List[i].Airport_FlyIn,
                                 Reises_List[i].Data_FlyOut,
                                 Reises_List[i].Data_FlyIn,
                                 Reises_List[i].PlaneMark,
                                 ref i, line, column);
                    i++;
                }

                if ((i + 1) % end_point == 0 && (i < Reises_List.Count))
                {
                    ReisTablBody(Reises_List[i].P_ReisCode,
                                 Reises_List[i].Airport_FlyOut,
                                 Reises_List[i].Airport_FlyIn,
                                 Reises_List[i].Data_FlyOut,
                                 Reises_List[i].Data_FlyIn,
                                 Reises_List[i].PlaneMark,
                                 ref i, line, column);                    
                }

                Console.WriteLine("╚═══════╩═══════════════════════════════════╩═══════════════════════════════════╩══════════════╩══════════════╩═════════════════════════════╝\n");



                var button = Console.ReadKey(true).Key;

                while (button != ConsoleKey.Escape &&
                       button != ConsoleKey.Enter &&
                       button != ConsoleKey.LeftArrow &&
                       button != ConsoleKey.RightArrow &&
                       button != ConsoleKey.DownArrow &&
                       button != ConsoleKey.UpArrow)
                {
                    button = Console.ReadKey(true).Key;
                }

                if (button == ConsoleKey.Escape)
                    return;
                else if (button == ConsoleKey.Enter)
                {
                    if (column == 0)
                        prices.PrintPrice(Reises_List[line].P_ReisCode);
                    else
                        airports.PrintAirports(Reises_List[line].Airport_FlyOut, Reises_List[line].Airport_FlyIn);
}
                else if(button == ConsoleKey.RightArrow)
                {
                    if (column < 2)
                    {
                        ++column;
                    } 
                    else
                    {
                        column = 0;
                    }
                }
                else if (button == ConsoleKey.LeftArrow)
                {
                    if ( column > 0)
                    {
                        --column;
                    }
                    else
                    {
                        column = 2;
                    }
                }
                else if (button == ConsoleKey.DownArrow)
                {
                    line++;

                    if ((line % end_point == 0) && (line < Reises_List.Count))
                    {
                        start_point += end_point;
                    }
                    if (line == Reises_List.Count)
                    {
                        line = 0;
                        start_point = 0;
                    }
                }
                else if (button == ConsoleKey.UpArrow)
                {
                    --line;

                    if (line < 0)
                    {
                        line = Reises_List.Count - 1;
                        start_point = (Reises_List.Count - 1) - ((Reises_List.Count - 1) % end_point);
                    }
                    else if ((line + 1) % end_point == 0)
                    {
                        start_point -= end_point;
                    }
                }


            }

        }

        #endregion


        // отрисовка тела таблицы
        #region ReisTablBody
        public void ReisTablBody(Prices rReisCode, Airports flyOut_Airport, Airports flyIn_Airport, DateTime flyOut_Date, DateTime flyIn_Date, string planeMark, ref int index, int line, int column)
        {

            string AFO_part1 = ""; // 1/2 аэропорта вылета
            string AFO_part2 = ""; // 2/2 аэропорта вылета

            string AFI_part1 = ""; // 1/2 аэропорта прилёта
            string AFI_part2 = ""; // 2/2 аэропорта прилёта

            string PlaneMark_1 = ""; // 1/2 марка самолёта
            string PlaneMark_2 = ""; // 2/2 марка самолёта



            Console.WriteLine("╠═══════╬═══════════════════════════════════╬═══════════════════════════════════╬══════════════╬══════════════╬═════════════════════════════╣");
            Print_Word(flyOut_Airport.Airport_Name, ref AFO_part1, ref AFO_part2, 30); // как выводить название аэропорта вылета ( с переносом ил нет)
            Print_Word(flyIn_Airport.Airport_Name, ref AFI_part1, ref AFI_part2, 30); // как выводить название аэропорта прилёта ( с переносом ил нет)
            Print_Word(planeMark, ref PlaneMark_1, ref PlaneMark_2, 25); // как выводить марку самолёта ( с переносом ил нет)

            if (line != index)
            {
                Console.WriteLine($"║  {rReisCode.ReisCode}{new string(' ', 5 - rReisCode.ReisCode.ToString().Length)}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");
                Console.WriteLine($"║       ║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");
            }
            else
            {
                switch (column)
                {
                    case 0: // закрасить КОД АЭРОПОРТА
                        Console.Write($"║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  { rReisCode.ReisCode}{new string(' ', 5 - rReisCode.ReisCode.ToString().Length)}");
                        Console.ResetColor();

                        Console.WriteLine($"║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");

                        Console.Write("║");
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write("       ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");

                        break;

                    case 1: // закрасить АЭРОПОРТ ПРИЛЁТА
                        Console.Write($"║  {rReisCode.ReisCode}{new string(' ', 5 - rReisCode.ReisCode.ToString().Length)}║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");
                        Console.Write("║       ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");

                        break;

                    case 2: // закрасить АЭРОПОРТ ВЫЛЕТА
                        Console.Write($"║  {rReisCode.ReisCode}{new string(' ', 5 - rReisCode.ReisCode.ToString().Length)}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");
                        Console.Write($"║       ║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ");
                        Console.ResetColor();

                        Console.WriteLine($"║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");

                        break;

                    case 3:
                        Console.Write($"║  {rReisCode.ReisCode}{new string(' ', 5 - rReisCode.ReisCode.ToString().Length)}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");

                        Console.Write($"║       ║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ");
                        Console.ResetColor();

                        Console.WriteLine($"║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");


                        break;

                    case 4:
                        Console.Write($"║  {rReisCode.ReisCode}{new string(' ', 5 - rReisCode.ReisCode.ToString().Length)}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");

                        Console.Write($"║       ║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");

                        break;

                    case 5:
                        Console.Write($"║  {rReisCode.ReisCode}{new string(' ', 5 - rReisCode.ReisCode.ToString().Length)}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ");
                        Console.ResetColor();

                        Console.WriteLine($"║");

                        Console.Write($"║       ║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ");
                        Console.ResetColor();

                        Console.WriteLine($"║");

                        break;

                    default:
                        break;
                }
            }
            
        }
        #endregion

        // метод, отвечающий за перенос на новую строку
        #region Print_Word
        private void Print_Word(string name, ref string part1, ref string part2, int lenght) 
        {
            if (name.Length < lenght)
            {
                part1 = name;
                part2 = "";
            }
            else
            {
                int spacebar = 0;
                part2 = "";
                foreach (char c in name)
                {
                    if (c == ' ' || c == '-')
                        spacebar++;
                    if (spacebar < 3 || part1.Length < lenght)
                        part1 += c;
                    else
                        part2 += c;                    
                }
                part2 = part2.Trim();
            }
        }
        #endregion
        // =============================================================================









        // =============================================================================

        // Удаление элемента таблицы
        #region DeleteInfo
        public void DeleteInfo(Airports airports, Prices prices)
        {
            bool exit = false;
            int start_point = 0;
            int line = 0;

            int column = 0;


            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                                  "║                                                               Таблица Рейсов                                                              ║\n" +
                                  "║                                                                                                                                           ║\n" +
                                  "╠═══════╦═══════════════════════════════════╦═══════════════════════════════════╦══════════════╦══════════════╦═════════════════════════════╣\n" +
                                  "║--код--║--        Аэропорт вылета        --║--        Аэропорт прилёта       --║-Дата и время-║-Дата и время-║--      Марка самолёта     --║\n" +
                                  "║-рейса-║                                   ║                                   ║    вылета    ║    вылета    ║                             ║");


                int i = start_point;
                while (((i + 1) % 20) > 0 && i < Reises_List.Count)
                {
                    ReisTablBody(Reises_List[i].P_ReisCode,
                                 Reises_List[i].Airport_FlyOut,
                                 Reises_List[i].Airport_FlyIn,
                                 Reises_List[i].Data_FlyOut,
                                 Reises_List[i].Data_FlyIn,
                                 Reises_List[i].PlaneMark,
                                 ref i, line, column);
                    i++;
                }

                Console.WriteLine("╚═══════╩═══════════════════════════════════╩═══════════════════════════════════╩══════════════╩══════════════╩═════════════════════════════╝\n");

                var button = Console.ReadKey(true).Key;

                while (button != ConsoleKey.Escape &&
                       button != ConsoleKey.Enter &&
                       button != ConsoleKey.LeftArrow &&
                       button != ConsoleKey.RightArrow &&
                       button != ConsoleKey.DownArrow &&
                       button != ConsoleKey.UpArrow)
                {
                    button = Console.ReadKey(true).Key;
                }

                if (button == ConsoleKey.Escape) // выйти
                    return;
                else if (button == ConsoleKey.Enter) // выбрать элемент
                {
                    if (column == 0) // удалить код рейса
                    {
                        bool sure = false;
                        if (sure.AreUSure("","Вы уверены, что хотите удалить выбранный элемент?"))
                        {
                            Reises_List[line].P_ReisCode.ReisCode = null;
                            Reises_List[line].P_ReisCode.BuisnessClass_Num = null;
                            Reises_List[line].P_ReisCode.BuisnessClass_Price = null;
                            Reises_List[line].P_ReisCode.EconomClass_Num = null;
                            Reises_List[line].P_ReisCode.EconomClass_Price = null;
                        }
                        
                    }
                    else if (column == 1) // удалить аэропорт вылета
                    {
                        bool sure = false;
                        if (sure.AreUSure("","Вы уверены, что хотите удалить выбранный элемент?"))
                        {
                            Reises_List[line].Airport_FlyOut.Airport_Code = "---";
                            Reises_List[line].Airport_FlyOut.Airport_City = "---";
                            Reises_List[line].Airport_FlyOut.Airport_Name = "---";
                        }
                    }
                    else if (column == 2) // удалить аэропорт прилёта
                    {
                        bool sure = false;
                        if (sure.AreUSure("","Вы уверены, что хотите удалить выбранный элемент?"))
                        {
                            Reises_List[line].Airport_FlyIn.Airport_Code = "---";
                            Reises_List[line].Airport_FlyIn.Airport_City = "---";
                            Reises_List[line].Airport_FlyIn.Airport_Name = "---";
                        }
                    }

                }
                else if (button == ConsoleKey.RightArrow) // вправо
                {
                    if (column < 2)
                    {
                        ++column;
                    }
                    else
                    {
                        column = 0;
                    }
                }
                else if (button == ConsoleKey.LeftArrow) // влево
                {
                    if (column > 0)
                    {
                        --column;
                    }
                    else
                    {
                        column = 2;
                    }
                }
                else if (button == ConsoleKey.DownArrow) // вниз
                {
                    if (line < Reises_List.Count)
                    {
                        if (line == Reises_List.Count - 1)
                        {
                            line = 0;
                        }
                        else if (line == 19)
                        {
                            start_point += 20;
                            ++line;
                        }
                        else
                            ++line;
                    }
                }
                else if (button == ConsoleKey.UpArrow) // вверх
                {
                    if (line >= 0)
                    {
                        if (line == 0)
                        {
                            line = Reises_List.Count - 1;
                        }
                        else if (line == 20)
                        {
                            start_point -= 20;
                            --line;
                        }
                        else
                            --line;

                    }
                }
            }
        }

        #endregion

        // =============================================================================









        
       /// <summary>
       /// РЕДАКТИРОВАНИЕ
       /// </summary>
       /// <param name="airports"></param>
       /// <param name="prices"></param>
        public void RedactInfo(Airports airports, Prices prices)
        {
            bool exit = false;
            int start_point = 0;
            int line = 0;

            int column = 0;


            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                                  "║                                                               Таблица Рейсов                                                              ║\n" +
                                  "║                                                                                                                                           ║\n" +
                                  "╠═══════╦═══════════════════════════════════╦═══════════════════════════════════╦══════════════╦══════════════╦═════════════════════════════╣\n" +
                                  "║--код--║--        Аэропорт вылета        --║--        Аэропорт прилёта       --║-Дата и время-║-Дата и время-║--      Марка самолёта     --║\n" +
                                  "║-рейса-║                                   ║                                   ║    вылета    ║    вылета    ║                             ║");


                int i = start_point;
                while (((i + 1) % 20) > 0 && i < Reises_List.Count)
                {
                    ReisTablBody(Reises_List[i].P_ReisCode,
                                 Reises_List[i].Airport_FlyOut,
                                 Reises_List[i].Airport_FlyIn,
                                 Reises_List[i].Data_FlyOut,
                                 Reises_List[i].Data_FlyIn,
                                 Reises_List[i].PlaneMark,
                                 ref i, line, column);
                    i++;
                }

                Console.WriteLine("╚═══════╩═══════════════════════════════════╩═══════════════════════════════════╩══════════════╩══════════════╩═════════════════════════════╝\n");

                var button = Console.ReadKey(true).Key;

                while (button != ConsoleKey.Escape &&
                       button != ConsoleKey.Enter &&
                       button != ConsoleKey.LeftArrow &&
                       button != ConsoleKey.RightArrow &&
                       button != ConsoleKey.DownArrow &&
                       button != ConsoleKey.UpArrow)
                {
                    button = Console.ReadKey(true).Key;
                }

                if (button == ConsoleKey.Escape) // выйти
                    return;
                else if (button == ConsoleKey.Enter) // выбрать элемент
                {
                    switch (column)
                    {
                        case 0: // РЕДАКТИРОВАТЬ КОД РЕЙСА
                            RedactCode(prices, Reises_List[line].P_ReisCode);
                            break;

                        case 1: // РЕДАКТИРОВАТЬ АЭРОПОРТ ВЫЛЕТА
                            bool exxxit = false;

                            while (!exxxit)
                            {
                                Console.Clear();
                                int indexofAirport;

                                if (Redact_Everywhere_OR_Once())
                                {
                                    int j = 0;
                                    for (j = 0; j < airports.Airport_List.Count; j++)
                                    {
                                        if (airports.Airport_List[j] == Reises_List[line].Airport_FlyIn)
                                            break;
                                    }

                                    indexofAirport = airports.ChooseCreatedAirport(j);
                                    if (indexofAirport == -1)
                                        exxxit = true;
                                    else
                                    {
                                        Reises_List[line].Airport_FlyOut = airports.Airport_List[indexofAirport];
                                        exxxit = true;
                                    }

                                }                                
                                else
                                {
                                    RedactAirport(Reises_List[line].Airport_FlyOut, Reises_List[line].Airport_FlyIn, airports);
                                    exxxit = true;
                                }
                            }
                            exxxit = false;

                            break;

                        case 2: // РЕДАКТИРОВАТЬ АЭРОПОРТ ПРИЛЁТА

                            exxxit = false;

                            while (!exxxit)
                            {
                                Console.Clear();
                                int indexofAirport;
                                if (Redact_Everywhere_OR_Once())
                                {
                                    int j = 0;
                                    for (j = 0; j < airports.Airport_List.Count; j++)
                                    {
                                        if (airports.Airport_List[j] == Reises_List[line].Airport_FlyOut)
                                            break;
                                    }

                                    indexofAirport = airports.ChooseCreatedAirport(j);
                                    if (indexofAirport == -1)
                                        exxxit = true;
                                    else
                                    {
                                        Reises_List[line].Airport_FlyIn = airports.Airport_List[indexofAirport];
                                        exxxit = true;
                                    }

                                }
                                else
                                {
                                    RedactAirport(Reises_List[line].Airport_FlyIn, Reises_List[line].Airport_FlyOut, airports);
                                    exxxit = true;
                                }
                            }
                            exxxit = false;
                            break;

                        case 3: // РЕДАКТИРОВАТЬ ДАТУ И ВРЕМЯ ВЫЛЕТА
                            RedactDate(ref Reises_List[line].Data_FlyOut, ref Reises_List[line].Data_FlyIn);
                            break;

                        case 4: // РЕДАКТИРОВАТЬ ДАТУ И ВРЕМЯ ПРИЛЁТА
                            RedactDate(ref Reises_List[line].Data_FlyOut, ref Reises_List[line].Data_FlyIn);
                            break;

                        case 5: // РЕДАКТИРОВАТЬ МАРКУ САМОЛЁТА
                            RedactPlaneMark(ref Reises_List[line].PlaneMark);
                            break;

                    }                    

                }
                else if (button == ConsoleKey.RightArrow) // вправо
                {
                    if (column < 5)
                    {
                        ++column;
                    }
                    else
                    {
                        column = 0;
                    }
                }
                else if (button == ConsoleKey.LeftArrow) // влево
                {
                    if (column > 0)
                    {
                        --column;
                    }
                    else
                    {
                        column = 5;
                    }
                }
                else if (button == ConsoleKey.DownArrow) // вниз
                {
                    if (line < Reises_List.Count)
                    {
                        if (line == Reises_List.Count - 1)
                        {
                            line = 0;
                        }
                        else if (line == 19)
                        {
                            start_point += 20;
                            ++line;
                        }
                        else
                            ++line;
                    }
                }
                else if (button == ConsoleKey.UpArrow) // вверх
                {
                    if (line >= 0)
                    {
                        if (line == 0)
                        {
                            line = Reises_List.Count - 1;
                        }
                        else if (line == 20)
                        {
                            start_point -= 20;
                            --line;
                        }
                        else
                            --line;

                    }
                }
            }
        }



        /// <summary>
        /// ИЗМЕНЕНИЕ КОДА РЕЦСА
        /// </summary>
        /// <param name="plist">список кодов рейсов</param>
        /// <param name="prices">изменяемый код рейса</param>
        private void RedactCode(Prices plist, Prices prices)
        {
            int? reisCode = prices.ReisCode;
            int? BC_Num = prices.BuisnessClass_Num;
            int? BC_Price = prices.BuisnessClass_Price;
            int? EC_Num = prices.EconomClass_Num;
            int? EC_Price = prices.EconomClass_Price;

            ConsoleKey button;

            bool exit = false;


            int line = 0;

            while (!exit)
            {
                switch (line)
                {
                    case 0:
                        Console.Clear();

                        #region ВЫДЕЛЕНИЕ КОДА РЕЙСА
                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ КОДА РЕЙСА
                        Console.Write("Код рейса:");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {reisCode} ");
                        Console.ResetColor();
                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ КОДА РЕЙСА

                        Console.WriteLine($"Количество мест Бизнес-класса: {BC_Num}\n" +
                                          $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                          $"Количество мест Эконом-класса: {EC_Num}\n" +
                                          $"Цена за место Эконом-класса: {EC_Price}\n");
                        #endregion

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:

                                #region EXIT
                                exit = exit.AreUSure($"Код рейса: {reisCode}\n" +
                                               $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                               $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                               $"Количество мест Эконом-класса: {EC_Num}\n" +
                                               $"Цена за место Эконом-класса: {EC_Price}\n",
                                               "Сохранить изменения?");
                                if (exit)
                                {
                                    prices.ReisCode = reisCode;
                                    prices.BuisnessClass_Num = BC_Num;
                                    prices.BuisnessClass_Price = BC_Price;
                                    prices.EconomClass_Num = EC_Num;
                                    prices.EconomClass_Price = EC_Price;
                                }

                                return;
                                #endregion

                                break;

                            case ConsoleKey.Enter:
                                
                                while (!exit)
                                {
                                    Console.Clear(); // очистка консоли

                                    Console.WriteLine($"Код рейса: \n" +
                                                      $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                                      $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                                      $"Количество мест Эконом-класса: {EC_Num}\n" +
                                                      $"Цена за место Эконом-класса: {EC_Price}\n");
                                    Console.SetCursorPosition(11, 0);

                                    RedactReisCode(plist.Prices_List, ref reisCode, ref exit);                                
                                }
                                exit = false;

                                break;

                            case ConsoleKey.DownArrow:
                                line++;

                                break;

                            case ConsoleKey.UpArrow:
                                line = 4;

                                break;

                        }


                        break;


                    case 1:
                        Console.Clear();

                        #region ВЫДЕЛЕНИЕ МЕСТ БИЗНЕС-КЛАССА
                        Console.WriteLine($"Код рейса: {reisCode}");

                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ МЕСТ БИЗНЕС-КЛАССА
                        Console.Write("Количество мест Бизнес-класса:");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {BC_Num} ");
                        Console.ResetColor();
                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ МЕСТ БИЗНЕС-КЛАССА

                        Console.WriteLine($"Цена за место Бизнес-класса: {BC_Price}\n" +
                                          $"Количество мест Эконом-класса: {EC_Num}\n" +
                                          $"Цена за место Эконом-класса: {EC_Price}\n");
                        #endregion


                        switch (PressedButton(out button))
                        {                            
                            case ConsoleKey.Escape:

                                #region EXIT
                                exit = exit.AreUSure($"Код рейса: {reisCode}\n" +
                                               $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                               $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                               $"Количество мест Эконом-класса: {EC_Num}\n" +
                                               $"Цена за место Эконом-класса: {EC_Price}\n",
                                               "Сохранить изменения?");
                                if (exit)
                                {
                                    prices.ReisCode = reisCode;
                                    prices.BuisnessClass_Num = BC_Num;
                                    prices.BuisnessClass_Price = BC_Price;
                                    prices.EconomClass_Num = EC_Num;
                                    prices.EconomClass_Price = EC_Price;
                                }

                                return;
                                #endregion

                                break;

                            case ConsoleKey.Enter:

                                while (!exit)
                                {
                                    Console.Clear();

                                    Console.WriteLine($"Код рейса: {reisCode}\n" +
                                                      $"Количество мест Бизнес-класса: \n" +
                                                      $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                                      $"Количество мест Эконом-класса: {EC_Num}\n" +
                                                      $"Цена за место Эконом-класса: {EC_Price}\n");
                                    Console.SetCursorPosition(31, 1);

                                    BC_Num = RedactPlacesPrices(500, ref exit);
                                }
                                exit = false;

                                break;

                            case ConsoleKey.DownArrow:
                                line++;

                                break;

                            case ConsoleKey.UpArrow:
                                line--;

                                break;
                        }

                        break;

                    case 2:
                        Console.Clear();

                        #region ВЫДЕЛЕНИЕ ЦЕН БИЗНЕС-КЛАССА
                        Console.WriteLine($"Код рейса: {reisCode}\n" +
                                                  $"Количество мест Бизнес-класса: {BC_Num}");

                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ ЦЕН БИЗНЕС-КЛАССА
                        Console.Write($"Цена за место Бизнес-класса:");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {BC_Price} ");
                        Console.ResetColor();
                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ ЦЕН БИЗНЕС-КЛАССА

                        Console.WriteLine($"Количество мест Эконом-класса: {EC_Num}\n" +
                                          $"Цена за место Эконом-класса: {EC_Price}\n");
                        #endregion

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:
                                
                                #region EXIT
                                exit = exit.AreUSure($"Код рейса: {reisCode}\n" +
                                               $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                               $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                               $"Количество мест Эконом-класса: {EC_Num}\n" +
                                               $"Цена за место Эконом-класса: {EC_Price}\n",
                                               "Сохранить изменения?");
                                if (exit)
                                {
                                    prices.ReisCode = reisCode;
                                    prices.BuisnessClass_Num = BC_Num;
                                    prices.BuisnessClass_Price = BC_Price;
                                    prices.EconomClass_Num = EC_Num;
                                    prices.EconomClass_Price = EC_Price;
                                }

                                return;
                                #endregion

                                break;

                            case ConsoleKey.Enter:

                                while (!exit)
                                {
                                    Console.Clear();

                                    Console.WriteLine($"Код рейса: {reisCode}\n" +
                                                      $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                                      $"Цена за место Бизнес-класса: \n" +
                                                      $"Количество мест Эконом-класса: {EC_Num}\n" +
                                                      $"Цена за место Эконом-класса: {EC_Price}\n");
                                    Console.SetCursorPosition(29, 2);

                                    BC_Price = RedactPlacesPrices(2000000, ref exit);
                                }
                                exit = false;

                                break;

                            case ConsoleKey.DownArrow:
                                line++;

                                break;

                            case ConsoleKey.UpArrow:
                                line--;

                                break;
                        }


                        break;

                    case 3:
                        Console.Clear();

                        #region ВЫДЕЛЕНИЕ МЕСТ ЭКОНОМ-КЛАССА
                        Console.WriteLine($"Код рейса: {reisCode}\n" +
                                                  $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                                  $"Цена за место Бизнес-класса: {BC_Price}");

                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ МЕСТ ЭКОНОМ-КЛАССА
                        Console.Write($"Количество мест Эконом-класса:");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {EC_Num} ");
                        Console.ResetColor();
                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ МЕСТ ЭКОНОМ-КЛАССА

                        Console.WriteLine($"Цена за место Эконом-класса: {EC_Price}\n");
                        #endregion

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:
                                #region EXIT
                                exit = exit.AreUSure($"Код рейса: {reisCode}\n" +
                                               $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                               $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                               $"Количество мест Эконом-класса: {EC_Num}\n" +
                                               $"Цена за место Эконом-класса: {EC_Price}\n",
                                               "Сохранить изменения?");
                                if (exit)
                                {
                                    prices.ReisCode = reisCode;
                                    prices.BuisnessClass_Num = BC_Num;
                                    prices.BuisnessClass_Price = BC_Price;
                                    prices.EconomClass_Num = EC_Num;
                                    prices.EconomClass_Price = EC_Price;
                                }

                                return;
                                #endregion
                                break;

                            case ConsoleKey.Enter:

                                while (!exit)
                                {
                                    Console.Clear();

                                    Console.WriteLine($"Код рейса: {reisCode}\n" +
                                                      $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                                      $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                                      $"Количество мест Эконом-класса: \n" +
                                                      $"Цена за место Эконом-класса: {EC_Price}\n");
                                    Console.SetCursorPosition(31, 3);

                                    EC_Num = RedactPlacesPrices(500, ref exit);
                                }
                                exit = false;

                                break;

                            case ConsoleKey.DownArrow:
                                line++;

                                break;

                            case ConsoleKey.UpArrow:
                                line--;

                                break;
                        }



                        break;

                    case 4:
                        Console.Clear();

                        #region ВЫДЕЛЕНИЕ ЦЕН ЭКОНОМ-КЛАССА
                        Console.WriteLine($"Код рейса: {reisCode}\n" +
                                                  $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                                  $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                                  $"Количество мест Эконом-класса: {EC_Num}");

                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ ЦЕН ЭКОНОМ-КЛАССА
                        Console.Write($"Цена за место Эконом-класса: ");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {EC_Price} ");
                        Console.ResetColor();
                        //------------------------------------------------------------ ВЫДЕЛЕНИЕ ЦЕН ЭКОНОМ-КЛАССА
                        #endregion

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:
                                exit = exit.AreUSure($"Код рейса: {reisCode}\n" +
                                               $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                               $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                               $"Количество мест Эконом-класса: {EC_Num}\n" +
                                               $"Цена за место Эконом-класса: {EC_Price}\n",
                                               "Сохранить изменения?");
                                if (exit)
                                {
                                    prices.ReisCode = reisCode;
                                    prices.BuisnessClass_Num = BC_Num;
                                    prices.BuisnessClass_Price = BC_Price;
                                    prices.EconomClass_Num = EC_Num;
                                    prices.EconomClass_Price = EC_Price;
                                }

                                return; break;

                            case ConsoleKey.Enter:

                                while (!exit)
                                {
                                    Console.Clear();

                                    Console.WriteLine($"Код рейса: {reisCode}\n" +
                                                      $"Количество мест Бизнес-класса: {BC_Num}\n" +
                                                      $"Цена за место Бизнес-класса: {BC_Price}\n" +
                                                      $"Количество мест Эконом-класса: {EC_Num}\n" +
                                                      $"Цена за место Эконом-класса: \n");
                                    Console.SetCursorPosition(29, 4);

                                    EC_Price = RedactPlacesPrices(2000000, ref exit);
                                }
                                exit = false;
                                break;

                            case ConsoleKey.DownArrow:
                                line = 0;

                                break;

                            case ConsoleKey.UpArrow:
                                line--;

                                break;
                        }



                        break;


                }
            }
        }


        /// <summary>
        /// ИЗМЕНЕНИЕ ИНФОРМАЦИИ ОБ АЭРОПОРТЕ
        /// </summary>
        /// <param name="airport1">аэропорт1</param>
        /// <param name="airport2">аэропорт2</param>
        private void RedactAirport(Airports airport1, Airports airport2, Airports all_airports)
        {
            int line = 0;
            bool exit = false;
            ConsoleKey button;

            string Iata = airport1.Airport_Code;
            string City = airport1.Airport_City;
            string Name = airport1.Airport_Name;

            while (!exit)
            {
                switch (line)
                {
                    case 0:
                        Console.Clear();

                        #region ВЫДЕЛЕНИЕ IATA-КОДА АЭРОПОРТА

                        Console.Write("IATA код аэропорта:");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {Iata} ");
                        Console.ResetColor();
                                                                                             
                        Console.WriteLine($"Город аэропорта: {City}\n" +
                                          $"Название аэропорта: {Name}\n");

                        #endregion

                        switch (PressedButton(out button))
                        {
                            
                            case ConsoleKey.Escape:
                                #region EXIT
                                exit = exit.AreUSure($"IATA код аэропорта: {Iata}\n" +
                                                     $"Город аэропорта: {City}\n" +
                                                     $"Название аэропорта: {Name}\n",
                                                     "Сохранить изменения?");
                                if (exit)
                                {
                                    airport1.Airport_Code = Iata;
                                    airport1.Airport_City = City;
                                    airport1.Airport_Name = Name;
                                }
                                return;
                                #endregion
                                break;

                            case ConsoleKey.Enter:

                                #region РЕДАКТИРОВАНИЕ IATA-КОДА АЭРОПОРТА

                                while (!exit)
                                {
                                    Console.Clear();

                                    Console.WriteLine($"IATA код аэропорта: \n" +
                                                      $"Город аэропорта: {City}\n" +
                                                      $"Название аэропорта: {Name}\n");
                                    Console.SetCursorPosition(20, 0);

                                    Iata = Console.ReadLine();

                                    if (Iata.isCorrectIATA())
                                    {
                                        Iata = Iata.ToUpper();
                                        exit = isRedactedCodeCreated(Iata, airport1.Airport_Code, all_airports.Airport_List);
                                    }

                                }
                                exit = false;

                                #endregion

                                break;

                            case ConsoleKey.DownArrow:

                                line++;

                                break;

                            case ConsoleKey.UpArrow:
                                line = 2;

                                break;

                        }

                        break;

                    case 1:
                        #region ВЫДЕЛЕНИЕ ГОРОДА АЭРОПОРТА
                        Console.Clear();

                        Console.WriteLine($"IATA код аэропорта: {Iata}");

                        Console.Write("Город аэропорта:");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {City} ");
                        Console.ResetColor();

                        Console.WriteLine($"Название аэропорта: {Name}\n");
                        #endregion

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:
                                #region EXIT
                                exit = exit.AreUSure($"IATA код аэропорта: {Iata}\n" +
                                                     $"Город аэропорта: {City}\n" +
                                                     $"Название аэропорта: {Name}\n",
                                                     "Сохранить изменения?");
                                if (exit)
                                {
                                    airport1.Airport_Code = Iata;
                                    airport1.Airport_City = City;
                                    airport1.Airport_Name = Name;
                                }
                                return;
                                #endregion

                                break;

                            case ConsoleKey.Enter:

                                #region РЕДАКТИРОВАНИЕ ГОРОДА АЭРОПОРТА

                                while (!exit)
                                {
                                    Console.Clear();

                                    Console.WriteLine($"IATA код аэропорта: {Iata}\n" +
                                                      $"Город аэропорта: \n" +
                                                      $"Название аэропорта: {Name}\n");
                                    Console.SetCursorPosition(17, 1);

                                    City = Console.ReadLine();
                                    City = City.Trim();
                                    exit = City.isCorrectString(3, 50);
                                }
                                exit = false;

                                #endregion

                                break;

                            case ConsoleKey.DownArrow:
                                line++;

                                break;

                            case ConsoleKey.UpArrow:
                                line--;

                                break;

                        }


                        break;


                    case 2:

                        #region ВЫДЕЛЕНИЕ НАЗВАНИЯ АЭРОПОРТА
                        Console.Clear();

                        Console.WriteLine($"IATA код аэропорта: {Iata}\n" +
                                          $"Город аэропорта: {City}");

                        Console.Write("Название аэропорта:");

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {Name} ");
                        Console.ResetColor();
                        #endregion

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:
                                #region EXIT
                                exit = exit.AreUSure($"IATA код аэропорта: {Iata}\n" +
                                                     $"Город аэропорта: {City}\n" +
                                                     $"Название аэропорта: {Name}\n",
                                                     "Сохранить изменения?");
                                if (exit)
                                {
                                    airport1.Airport_Code = Iata;
                                    airport1.Airport_City = City;
                                    airport1.Airport_Name = Name;
                                }
                                return;
                                #endregion
                                
                                break;

                            case ConsoleKey.Enter:

                                #region РЕДАКТИРОВАТЬ НАЗВАНИЕ АЭРОПОРТА

                                while (!exit)
                                {
                                    Console.Clear();

                                    Console.WriteLine($"IATA код аэропорта: {Iata}\n" +
                                                      $"Город аэропорта: {City}\n" +
                                                      $"Название аэропорта: \n");
                                    Console.SetCursorPosition(20, 2);

                                    Name = Console.ReadLine();
                                    Name = Name.Trim();
                                    exit = Name.isCorrectString(3, 50);

                                }
                                exit = false;

                                #endregion

                                break;

                            case ConsoleKey.DownArrow:
                                line = 0;

                                break;

                            case ConsoleKey.UpArrow:
                                line--;

                                break;

                        }

                        break;
                }
            }
        }
        

        /// <summary>
        /// РЕДАКТИРОВАНИЕ КОДА АЭРОПОРТА
        /// </summary>
        /// <param name="reisCode">итоговый код</param>
        /// <param name="exit">условие выхода</param>
        private void RedactReisCode(List<Prices> plist, ref int? reisCode, ref bool exit)
        {
            int promeghutochnoe;
            string input;

            int equal = 0;
            bool isEqual = false;


            input = Console.ReadLine();
            input = input.Trim();

            if (string.IsNullOrWhiteSpace(input)) // проверка на корректность введённых данных
            {
                Console.WriteLine("\n\n\n\n\nПоле кода рейса не заполнено. Введите 3 цифры! \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
            }
            else if (input.Length > 3)
            {
                Console.WriteLine("\n\n\n\n\nКод рейса слишком длинный. Введите 3 цифры! \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
            }
            else if (input.Length < 3)
            {
                Console.WriteLine("\n\n\n\n\nКод рейса слишком короткий. Введите 3 цифры! \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
            }
            else if (!int.TryParse(input, out promeghutochnoe))
            {
                Console.WriteLine("\n\n\n\n\nВ коде присутствуют запрещённые символы. Введите 3 цифры! \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
            }
            else // код рейса введён верно
            {
                
                for (int i = 0; i < plist.Count; i++)
                {
                    if (promeghutochnoe == plist[i].ReisCode && reisCode!= promeghutochnoe)
                        equal++;

                    if (equal > 0)
                    {
                        Console.WriteLine("\n\n\n\n\nТакой код рейса уже существует! Введите новый код рейса.\n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        isEqual = true;
                        Console.ReadKey();
                        break;
                    }
                }

                if (!isEqual)
                {
                    reisCode = promeghutochnoe;
                    exit = true; // то ливаем и идём дальше
                }

                return;
            }        
            exit = false;
        }

        /// <summary>
        /// ИЗМЕНЕНИЕ КОЛ-ВА МЕСТ/ЦЕН
        /// </summary>
        /// <param name="maxNum">максимальное число</param>
        /// <param name="exit">условие выхода</param>
        /// <returns></returns>
        private int RedactPlacesPrices(int maxNum, ref bool exit)
        {
            int answer; // это вернётся, если всё ок
            string inpit = Console.ReadLine(); // строка, чтобы её преобразовать в int
            inpit = inpit.Trim();
            exit = false;

            if (string.IsNullOrWhiteSpace(inpit)) // пусто
            {
                Console.WriteLine("\n\nПоле не заполнено! Заполните поле. \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return -1;
            }
            else if (!int.TryParse(inpit, out answer)) // не число
            {
                Console.WriteLine("\n\nПожалуйста, введите число. Заполните поле заново. \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return -1;
            }
            else if (answer < 0) // слишком маленькое
            {
                Console.WriteLine("\n\nЧисло не может быть отрицательным. Заполните поле заново. \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return -1;
            }
            else if (answer > maxNum) // слишком большое
            {
                Console.WriteLine("\n\nВведённое число слишком большое. Заполните поле заново. \n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return -1;
            }
            else
            {
                exit = true;
                return answer;
            }
        }

        /// <summary>
        /// ИЗМЕНЕНИЕ ДАТЫ (ВЫЛЕТА или ПРИЛЁТА)
        /// </summary>
        /// <param name="date1">дата вылета</param>
        /// <param name="date2">дата прилёта</param>
        private void RedactDate(ref DateTime date1, ref DateTime date2)
        {
            DateTime time1 = new DateTime();
            time1 = date1;
            DateTime time2 = new DateTime();
            time2 = date2;
            int line = 0;
            ConsoleKey button;
            bool exit = false;

            while (!exit)
            {
                switch (line)
                {
                    case 0: // изменение даты вылета
                        Console.Clear();

                        Console.Write("Дата вылета:");
                        Console.BackgroundColor = ConsoleColor.DarkRed;                        
                        Console.WriteLine($" {time1.Day:00}.{time1.Month:00}.{time1.Year}   {time1.Hour:00}:{time1.Minute:00} ");
                        Console.ResetColor();

                        Console.WriteLine($"Дата прилёта: {time2.Day:00}.{time2.Month:00}.{time2.Year}   {time2.Hour:00}:{time2.Minute:00}");

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:

                                exit = exit.AreUSure($"Дата вылета: {time1.Day:00}.{time1.Month:00}.{time1.Year}   {time1.Hour:00}:{time1.Minute:00}\n" +
                                                     $"Дата прилёта: {time2.Day:00}.{time2.Month:00}.{time2.Year}   {time2.Hour:00}:{time2.Minute:00}",
                                                      "Сохранить изменения?");

                                if (exit)
                                {
                                    exit = isCorrect_Difference(time1, time2);
                                    if (exit)
                                    {
                                        date1 = time1;
                                        date2 = time2;
                                        return;
                                    }
                                    else
                                    {
                                        exit = false;
                                    }
                                }
                                else
                                {
                                    return;
                                }

                                break;

                            case ConsoleKey.Enter:

                                while (!exit)
                                {
                                    Console.Clear();
                                    try
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine($"Дата прилёта: {time2.Day:00}.{time2.Month:00}.{time2.Year}   {time2.Hour:00}:{time2.Minute:00}");

                                        Console.SetCursorPosition(0, 0);


                                        exit = isCorrect_Date(out time1);

                                    }
                                    catch
                                    {
                                        Console.WriteLine("\n\nДанные введены не по образцу. Повторите попытку.\n" +
                                                          "Нажмите любую клавишу, чтобы продолжить...");
                                        Console.ReadKey(true);

                                    }

                                }
                                exit = false;

                                break;

                            case ConsoleKey.DownArrow:
                                line = 1;
                                break;

                            case ConsoleKey.UpArrow:
                                line = 1;
                                break;                            
                        }


                        break;

                    case 1: // изменение даты прилёта

                        Console.Clear();

                        Console.WriteLine($"Дата вылета: {time1.Day:00}.{time1.Month:00}.{time1.Year}   {time1.Hour:00}:{time1.Minute:00}");

                        Console.Write("Дата прилёта:");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($" {time2.Day:00}.{time2.Month:00}.{time2.Year}   {time2.Hour:00}:{time2.Minute:00} ");
                        Console.ResetColor();

                        switch (PressedButton(out button))
                        {
                            case ConsoleKey.Escape:

                                exit = exit.AreUSure($"Дата вылета: {time1.Day:00}.{time1.Month:00}.{time1.Year}   {time1.Hour:00}:{time1.Minute:00}\n" +
                                                     $"Дата прилёта: {time2.Day:00}.{time2.Month:00}.{time2.Year}   {time2.Hour:00}:{time2.Minute:00}",
                                                      "Сохранить изменения?");

                                if (exit)
                                {
                                    exit = exit && isCorrect_Difference(time1, time2);
                                    if (exit)
                                    {
                                        date1 = time1;
                                        date2 = time2;

                                        return;
                                    }
                                    else
                                    {
                                        exit = false;
                                    }

                                }
                                else
                                {
                                    return;
                                }


                                break;

                            case ConsoleKey.Enter:

                                while (!exit)
                                {
                                    Console.Clear();
                                    try
                                    {
                                        Console.WriteLine($"Дата вылета: {time1.Day:00}.{time1.Month:00}.{time1.Year}   {time1.Hour:00}:{time1.Minute:00}");
                                        Console.WriteLine("");

                                        Console.SetCursorPosition(0, 1);

                                        exit = isCorrect_Date(out time2);

                                    }
                                    catch
                                    {

                                        Console.WriteLine("\n\nДанные введены не по образцу. Повторите попытку.\n" +
                                                          "Нажмите любую клавишу, чтобы продолжить...");
                                        Console.ReadKey(true);

                                    }

                                }
                                exit = false;

                                break;

                            case ConsoleKey.DownArrow:
                                line = 0;
                                break;

                            case ConsoleKey.UpArrow:
                                line = 0;
                                break;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// УСЛОВИЯ ИЗМЕНЕНИЯ ДАТЫ ВЫЛЕТА
        /// </summary>
        /// <param name="FlyOut_date">дата вылета</param>
        /// <param name="FlyIn_date">дата прилёта</param>
        /// <returns></returns>
        private static bool isCorrectRedactedDateFlyOut(DateTime FlyOut_date, DateTime FlyIn_date)
        {
            if (FlyIn_date < FlyOut_date) // дата прилёта раньше даты вылета
            {
                Console.WriteLine("\n\nДата вылета не может быть позже даты прилёта. Заполните поле снова.\n" +
                                  "Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else // дата вылета позже даты вылета
            {
                var DifferenceInTime = FlyIn_date - FlyOut_date; // разница между вылетом и прилётом
                DateTime DIfferenceDate = new DateTime(DifferenceInTime.Ticks); // преобразование разницы в экземпляр DateTime

                int hour = DIfferenceDate.Hour; // разница в часах

                if (hour > 0 && hour <= 18) // время полёта 1-18 часов (верно)
                    return true;
                else if (hour < 1) // время полёта <1 часа
                {
                    Console.WriteLine("\nВремя полёта слишком короткое \n" +
                                      "Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey(true);

                    return false;
                }
                else if (FlyIn_date.Hour <= FlyOut_date.Hour) // время прилёта 
                {
                    Console.WriteLine("\nВремя прилёта не может быть раньше времени вылета. Заполните поле снова.\n" +
                                      "Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.WriteLine("\nВремя полёта слишком большое \n" +
                                      "Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey(true);

                    return false;
                }

            }
        }
        

        /// <summary>
        /// ИЗМЕНЕНИЕ МАРКИ САМОЛЁТА
        /// </summary>
        /// <param name="planeMark">передача марки самолёта</param>
        private void RedactPlaneMark(ref string planeMark)
        {
            string planemarkk = planeMark;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                Console.Write("Марка самолёта:");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($" {planemarkk} ");
                Console.ResetColor();

                var button = Console.ReadKey(true).Key;
                while (button!= ConsoleKey.Escape && button != ConsoleKey.Enter)
                {
                    button = Console.ReadKey(true).Key;
                }

                switch (button)
                {
                    case ConsoleKey.Escape:
                        exit = exit.AreUSure($"Марка самолёта: {planemarkk}",
                                              "Сохранить изменения?");

                        if (exit)
                            planeMark = planemarkk;

                        return;

                        break;

                    case ConsoleKey.Enter:

                        while (!exit)
                        {
                            Console.Clear();

                            Console.Write("Марка самолёта: ");

                            planemarkk = Console.ReadLine();
                            planemarkk = planemarkk.Trim();
                            if (planemarkk.isCorrectPlaneMark())
                            {
                                exit = true;
                            }
                        }
                        exit = false;

                        break;
                }


            }

        }
        

        /// <summary>
            /// РЕДАКТИРОВАНИЕ КОДА АЭРОПОРТА
            /// </summary>
            /// <param name="air_code">Изменённый код (можно ввести тот, что был до этого</param>
            /// <param name="not_redact">Изначальный код</param>
            /// <param name="Airports_List">список аэропортов</param>
            /// <returns></returns>
        private bool isRedactedCodeCreated(string air_code, string not_redact, List<Airports> Airports_List)
        {
            foreach (Airports air in Airports_List)
            {
                if (air.Airport_Code == air_code && air_code != not_redact)
                {
                    Console.WriteLine("\n\n\nАэропорт с таким кодом уже существует. Введите новый код.\n" +
                                      "Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey(true);
                    return false;
                }
            }
            return true;
        }


        private bool DDD(ref DateTime inputDateTime)
        {
            try
            {
                string dateStr = Console.ReadLine();
                inputDateTime = DateTime.ParseExact(dateStr, "dd'.'MM'.'yyyy' 'HH':'mm", CultureInfo.CurrentCulture);

                if ((inputDateTime.Year - DateTime.Now.Year) >= -2 && (inputDateTime.Year - DateTime.Now.Year) <= 2)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nГод указан некорректно. Повторите попытку.");
                    Console.ReadKey(true);
                    return false;
                }


            }
            catch
            {
                Console.WriteLine("\n\nДанные введены не по образцу. Повторите попытку.\n" +
                                          "Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey(true);
                inputDateTime = DateTime.MinValue;
                return false;
            }


        }

        /// <summary>
        /// Редактировать аэропорт только здесь или везде
        /// </summary>
        /// <param name="infoUP"></param>
        /// <param name="qa"></param>
        /// <returns></returns>
        private bool Redact_Everywhere_OR_Once(string infoUP = "", string qa = "")
        {
            // выбор (от него зависит, что выведет)
            byte myChoise = 1;
            while (true) // цикл отображения ДА / НЕТ
            {
                Console.Clear();
                if (qa != "")
                {
                    Console.WriteLine(infoUP);
                    Console.WriteLine(qa);
                }
                else
                {
                    Console.WriteLine(infoUP);
                    Console.WriteLine("Изменить только этот элемент или все?");
                }
                switch (myChoise) // что вывести в зависимости от значения myChoise
                {
                    #region ДА
                    case 1: //------------------------------------------------ выделяем вариант ДА
                        Console.Write("   ");
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(" Только этот ");
                        Console.ResetColor();
                        Console.WriteLine(" |  Все"); //---------------------- всё покрасилось красиво

                        var button = Console.ReadKey(true).Key; // чтение клавиши и проверка на корректность
                        while (button != ConsoleKey.Enter && button != ConsoleKey.RightArrow && button != ConsoleKey.LeftArrow)
                        {
                            button = Console.ReadKey(true).Key; // изменение если неправильная клавиша
                        }

                        if (button == ConsoleKey.RightArrow || button == ConsoleKey.LeftArrow) // если <-|-> то
                        {
                            myChoise = 2; // выделяем вариант НЕТ
                        }
                        else if (button == ConsoleKey.Enter)
                        {
                            return true;
                        }
                        break;
                    #endregion

                    #region НЕТ
                    case 2:
                        Console.Write("    Только этот  | ");//------------------------ выделяем вариант НЕТ
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" Все ");
                        Console.ResetColor();//------------------------------- всё покрасилось красиво

                        button = Console.ReadKey(true).Key; // чтение клавиши + проверка
                        while (button != ConsoleKey.Enter && button != ConsoleKey.RightArrow && button != ConsoleKey.LeftArrow)
                        {
                            button = Console.ReadKey(true).Key; // изменение если неправильная клавиша
                        }

                        if (button == ConsoleKey.RightArrow || button == ConsoleKey.LeftArrow) // если <-|->, то 
                        {
                            myChoise = 1; // переходим на вариант ДА
                        }
                        else if (button == ConsoleKey.Enter)
                        {
                            return false;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }

            }

        }










        


        /// <summary>
        /// КНОПКА нажата ESCAPE, ENTER, DOWN, UP
        /// </summary>
        /// <param name="button">нажатая кнопка кнопка</param>
        /// <returns></returns>
        private ConsoleKey PressedButton(out ConsoleKey button)
        {
            button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            return button;
        }
    }
}
