using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Taiwan Taoyuan International (Chiang Kai Shek International)
// Piper PA-31 Navajo Chieftain - Navajo Chieftain
namespace Logic
{
    public class Reis
    {
        public Price P_ReisCode { get; set; }

        public Airport Airport_FlyOut;
        public Airport Airport_FlyIn;

        public DateTime Data_FlyOut;
        public DateTime Data_FlyIn;

        public string PlaneMark = "";

        
        
        // =============================================================================
        #region ДОБАВЛЕНИЕ
        // Добавление рейса
        #region AddReise
        public void AddReise(List<Price> Prices_List, List<Airport> Airports_List, List<Reis> Reises_List)
        {
            Airport airport = new Airport();
            Price p_reiscode = new Price();

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
            
            bool allright = false, exit = false;



            while (!allright)
            {
                #region Аэропорт вылета
                while (!exit)
                {
                    switch (NewOrCreated("ESC - выйти\n---> Добавление Рейса <---\n\n\n\n\n\n\n\nАэропорт вылета: ", ref exit))
                    {                        
                        case 1:

                            airport.Airport_Adding(ref Airports_List); // предпоследний в списке (predposl)
                            FlyOut_ind = Airports_List.Count - 1;
                            isNewAirportFlyOut = true;

                            break;

                        case 2:

                            if (Airports_List.Count >= 1)
                            {
                                FlyOut_ind = airport.ChooseCreatedAirport(Airports_List);
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

                            break;

                        case 3:
                            if (Airports_List.Count>0)
                                Airports_List.Remove(Airports_List.Last());
                            return;
                            break;
                    }
                        
                }
                exit = false;
                #endregion

                #region Аэропорт прилёта
                while (!exit)
                {
                    switch (
                            NewOrCreated("ESC - выйти\n---> Добавление Рейса <---\n\n" +
                                         "Аэропорт вылета: " + Airports_List[FlyOut_ind].Airport_Name + "\n\n\n\n\n" +
                                         "Аэропорт прилёта: ", ref exit)
                           )
                    {
                        case 1:

                            airport.Airport_Adding(ref Airports_List); // предпоследний в списке (predposl)
                            FlyIn_ind = Airports_List.Count - 1;
                            isNewAirportFlyIn = true;

                            break;
                        case 2:
                            if (Airports_List.Count >= 2)
                            {
                                FlyIn_ind = airport.ChooseCreatedAirport(Airports_List, FlyOut_ind);
                                if (FlyIn_ind >= 0 && FlyIn_ind != FlyOut_ind)
                                    exit = true;
                            }
                            else
                            {
                                Console.WriteLine("\nСписок аэропортов не достаточно большой. Введите хотя бы 2 аэропорта.\n" +
                                                  "Нажмите любую клавишу, чтобы продолжить...");
                                exit = false;
                                Console.ReadKey(true);
                            }

                            break;

                        case 3:
                            Airports_List.RemoveAt(Airports_List.Count - 1);
                            return;
                            break;
                    }

                }
                exit = false;
                #endregion



                #region Код рейса + цена + места
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("ESC - выйти\n---> Добавление Рейса <---\n\n" +
                                      "Аэропорт вылета: " + Airports_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + Airports_List[FlyIn_ind].Airport_Name + "\n\n\n\n");

                    Console.Write("Ввод кода рейса... Нажмите любую кнопку, чтобы продолжить.");
                    var b = Console.ReadKey(true).Key;
                    if (b != ConsoleKey.Escape)
                    {
                        Console.Clear();
                        p_reiscode.AddPriceForReis(Prices_List); // последний в списке
                        
                        exit = true;
                    }
                    else
                    {
                        Airports_List.RemoveAt(Airports_List.Count-1); // удаляем только что введённый аэропорт прилёта
                        Airports_List.RemoveAt(Airports_List.Count-1); // удаляем ранее введённый аэропорт вылета
                        return;
                    }
                        
                    
                }
                exit = false;
                #endregion



                #region Дата вылета
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n\n" +
                                      "Аэропорт вылета: " + Airports_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + Airports_List[FlyIn_ind].Airport_Name + "\n" +
                                      "Код рейса: " + Prices_List.Last().ReisCode + "\n\n\n");
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
                                      "Аэропорт вылета: " + Airports_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + Airports_List[FlyIn_ind].Airport_Name + "\n" +
                                      "Код рейса: " + Prices_List.Last().ReisCode + "\n" +
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
                                      "Аэропорт вылета: " + Airports_List[FlyOut_ind].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + Airports_List[FlyIn_ind].Airport_Name + "\n" +
                                      "Код рейса: " + Prices_List.Last().ReisCode + "\n" +
                                      $"Дата вылета: {FlyOut_data.Day:00}.{FlyOut_data.Month:00}.{FlyOut_data.Year}   {FlyOut_data.Hour:00}:{FlyOut_data.Minute:00}\n" +
                                      $"Дата прилёта: {FlyIn_data.Day:00}.{FlyIn_data.Month:00}.{FlyIn_data.Year}   {FlyIn_data.Hour:00}:{FlyIn_data.Minute:00}\n");

                    Console.Write("Марка самолёта: ");

                    planemark = Console.ReadLine();
                    planemark = planemark.Trim();
                    if (planemark.isCorrectPlaneMark())
                    {
                        Console.Clear();

                        exit = exit.AreUSure("---> Добавление Рейса <---\n\n" +
                                                     "Аэропорт вылета: " + Airports_List[Airports_List.Count - 2].Airport_Name + "\n" +
                                                     "Аэропорт прилёта: " + Airports_List.Last().Airport_Name + "\n" +
                                                     "Код рейса: " + Prices_List.Last().ReisCode + "\n" +
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
                                             "Аэропорт вылета: " + Airports_List[Airports_List.Count - 2].Airport_Name + "\n" +
                                             "Аэропорт прилёта: " + Airports_List.Last().Airport_Name + "\n" +
                                             "Код рейса: " + Prices_List.Last().ReisCode + "\n" +
                                             $"Дата вылета: {FlyOut_data.Day}.{FlyOut_data.Month}.{FlyOut_data.Year}   {FlyOut_data.Hour}:{FlyOut_data.Minute}\n" +
                                             $"Дата прилёта: {FlyIn_data.Day}.{FlyIn_data.Month}.{FlyIn_data.Year}   {FlyIn_data.Hour}:{FlyIn_data.Minute}\n" +
                                             "Марка самолёта: " + planemark + "\n\n");
                if (!allright)
                {
                    if (isNewAirportFlyOut && isNewAirportFlyIn)
                    {
                        Airports_List.RemoveAt(Airports_List.Count - 1);
                        Airports_List.RemoveAt(Airports_List.Count - 1);
                    }
                    else if (isNewAirportFlyOut || isNewAirportFlyIn)
                    {
                        Airports_List.RemoveAt(Airports_List.Count - 1);
                    }
                                       
                    Prices_List.RemoveAt(Prices_List.Count - 1);
                }
            }
            exit = false;
            #endregion

            // добавление рейса
            Reises_List.Add(new Reis { Airport_FlyOut = Airports_List[FlyOut_ind]
                                      ,Airport_FlyIn = Airports_List[FlyIn_ind]
                                      ,P_ReisCode = Prices_List.Last()
                                      ,Data_FlyOut = FlyOut_data
                                      ,Data_FlyIn = FlyIn_data
                                      ,PlaneMark = planemark});            

        }
        #endregion
        
        // Выбор: создать новый аэропорт или выбрать из существующих
        #region NewOrCreated airport
        /// <summary>
        /// Новый аэропорт или уже созданный?
        /// </summary>
        /// <param name="info">верхняя строка</param>
        /// <param name="smallExit">условие выхода (что-то выбрано)</param>
        /// <returns></returns>
        private static sbyte NewOrCreated(string info, ref bool smallExit)
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
                        while (button != ConsoleKey.Enter && button != ConsoleKey.RightArrow && button != ConsoleKey.LeftArrow && button != ConsoleKey.Escape)
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
                            return 1;
                        }
                        else if (button == ConsoleKey.Escape)
                        {
                            return 3;
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
                        while (button != ConsoleKey.Enter && button != ConsoleKey.RightArrow && button != ConsoleKey.LeftArrow && button != ConsoleKey.Escape)
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
                            return 2;
                        }
                        else if (button == ConsoleKey.Escape)
                        {
                            return 3;
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
                Console.Write("Введите дату по образцу <ДД.ММ.ГГГГ чч:мм>: ");
                string dateStr = Console.ReadLine();
                inputDateTime = DateTime.ParseExact(dateStr, "dd'.'MM'.'yyyy' 'HH':'mm", CultureInfo.CurrentCulture);
                
                if ((inputDateTime.Year - DateTime.Now.Year) >= -2 && (inputDateTime.Year - DateTime.Now.Year) <= 2)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nСлишком большой временной отрезок. Повторите попытку.");
                    Console.ReadKey(true);
                    return false;
                }

            }           
            catch (FormatException)
            {
                Console.WriteLine("\n\nНеверный формат даты или времени. Повторите попытку.\n" +
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

        #endregion
        // =============================================================================




        // =============================================================================
        #region ОТРИСОВКА ТАБЛИЦЫ

        // Вывод таблицы
        #region PrintReises

        public void PrintReises(List<Reis> Reises_List, string input = "", bool isSearch = false, bool isFind = false)
        {
            Airport airport = new Airport();
            Price price = new Price();
            bool exit = false;
            int start_point = 0;
            int end_point = 10;

            int line = 0;            
            int column = 0;


            while (!exit)
            {
                Console.Clear();

                if (isSearch)
                {
                    Console.Write("   ");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("Поиск");
                    Console.ResetColor();
                    Console.Write(": " + input);
                    Console.WriteLine("\n\n");
                }              

                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                                  "║                                                               Таблица Рейсов                                                              ║\n" +
                                  "║                                                                                                                                           ║\n" +
                                  "╠═══════╦═══════════════════════════════════╦═══════════════════════════════════╦══════════════╦══════════════╦═════════════════════════════╣\n" +
                                  "║  код  ║          Аэропорт вылета          ║          Аэропорт прилёта         ║ Дата и время ║ Дата и время ║        Марка самолёта       ║\n" +
                                  "║ рейса ║                                   ║                                   ║    вылета    ║    вылета    ║                             ║");


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

                Console.WriteLine("╚═══════╩═══════════════════════════════════╩═══════════════════════════════════╩══════════════╩══════════════╩═════════════════════════════╝\n" +
                                  "ESC - выйти\n");

                if (isFind)
                    return;


                var button = Console.ReadKey(true).Key;

                while (button != ConsoleKey.Escape &&
                       button != ConsoleKey.Enter &&
                       button != ConsoleKey.LeftArrow &&
                       button != ConsoleKey.RightArrow &&
                       button != ConsoleKey.DownArrow &&
                       button != ConsoleKey.UpArrow &&
                       button != ConsoleKey.PageUp &&
                       button != ConsoleKey.PageDown)
                {
                    button = Console.ReadKey(true).Key;
                }

                if (button == ConsoleKey.Escape)
                    return;
                else if (button == ConsoleKey.Enter)
                {
                    if (column == 0)
                        price.PrintPrice(Reises_List[line].P_ReisCode);
                    else
                        airport.PrintAirports(Reises_List[line].Airport_FlyOut, Reises_List[line].Airport_FlyIn);
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
                else if (button == ConsoleKey.PageUp)
                {
                    start_point -= end_point;
                    line = start_point;


                    if (start_point < 0)
                    {
                        line = Reises_List.Count - 1;
                        start_point = (Reises_List.Count - 1) - ((Reises_List.Count - 1) % end_point);
                    }
                    
                }
                else if (button == ConsoleKey.PageDown)
                {
                    start_point += end_point;
                    line = start_point;
                    
                    if (start_point >= Reises_List.Count)
                    {
                        line = 0;
                        start_point = 0;
                    }
                }


            }

        }

        #endregion
        
        // отрисовка тела таблицы
        #region ReisTablBody
        public void ReisTablBody(Price rReisCode, Airport flyOut_Airport, Airport flyIn_Airport, DateTime flyOut_Date, DateTime flyIn_Date, string planeMark, ref int index, int line, int column = -1)
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
            Print_Word(planeMark, ref PlaneMark_1, ref PlaneMark_2, 25); // как выводить марку самолёта ( с переносом или нет)

            if (line != index)
            {
                Console.WriteLine($"║{rReisCode.ReisCode}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");
                Console.WriteLine($"║       ║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");
            }
            else
            {
                switch (column)
                {
                    case -1: // ничего не закрашивать (вывод для пункта "СОРТИРОВКА")
                        Console.WriteLine($"║{rReisCode.ReisCode}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");

                        Console.WriteLine($"║       ║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");

                        break;

                    case 0: // закрасить КОД АЭРОПОРТА
                        Console.Write($"║");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{rReisCode.ReisCode}");
                        Console.ResetColor();

                        Console.WriteLine($"║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║  {PlaneMark_1}{new string(' ', 25 - PlaneMark_1.Length)}  ║");

                        Console.Write("║");
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write("       ");
                        Console.ResetColor();

                        Console.WriteLine($"║  {AFO_part2}{new string(' ', 30 - AFO_part2.Length)}   ║  {AFI_part2}{new string(' ', 30 - AFI_part2.Length)}   ║    {flyOut_Date.Hour:00}:{flyOut_Date.Minute:00}     ║    {flyIn_Date.Hour:00}:{flyIn_Date.Minute:00}     ║  {PlaneMark_2}{new string(' ', 25 - PlaneMark_2.Length)}  ║");

                        break;

                    case 1: // закрасить АЭРОПОРТ ПРИЛЁТА
                        Console.Write($"║{rReisCode.ReisCode}║");

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
                        Console.Write($"║{rReisCode.ReisCode}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║");

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
                        Console.Write($"║{rReisCode.ReisCode}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║");

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
                        Console.Write($"║{rReisCode.ReisCode}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║");

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
                        Console.Write($"║{rReisCode.ReisCode}║  {AFO_part1}{new string(' ', 30 - AFO_part1.Length)}   ║  {AFI_part1}{new string(' ', 30 - AFI_part1.Length)}   ║  {flyOut_Date.Day:00}.{flyOut_Date.Month:00}.{flyOut_Date.Year}  ║  {flyIn_Date.Day:00}.{flyIn_Date.Month:00}.{flyIn_Date.Year}  ║");

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
                    if (part1.Length < lenght)
                        part1 += c;
                    else
                        part2 += c;                    
                }
                part2 = part2.Trim();
            }
        }
        #endregion

        #endregion
        // =============================================================================




        // =============================================================================
        #region УДАЛЕНИЕ
        public void DeleteInfo(List<Price> Prices_List, List<Reis> Reises_List, Price prices)
        {
            Airport airport = new Airport();


            bool exit = false;
            int start_point = 0;
            int end_point = 10;

            int line = 0;
            int column = 0;


            while (!exit)
            {
                Console.Clear();


                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                                  "║                                                               Таблица Рейсов                                                              ║\n" +
                                  "║                                                                                                                                           ║\n" +
                                  "╠═══════╦═══════════════════════════════════╦═══════════════════════════════════╦══════════════╦══════════════╦═════════════════════════════╣\n" +
                                  "║  код  ║          Аэропорт вылета          ║          Аэропорт прилёта         ║ Дата и время ║ Дата и время ║        Марка самолёта       ║\n" +
                                  "║ рейса ║                                   ║                                   ║    вылета    ║    вылета    ║                             ║");


                int i = start_point;
                while (((i + 1) % end_point > 0) && (i < Reises_List.Count))
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

                Console.WriteLine("╚═══════╩═══════════════════════════════════╩═══════════════════════════════════╩══════════════╩══════════════╩═════════════════════════════╝\n" +
                                  "ESC - выйти\n");


                var button = Console.ReadKey(true).Key;

                while (button != ConsoleKey.Escape &&
                       button != ConsoleKey.Enter &&
                       button != ConsoleKey.LeftArrow &&
                       button != ConsoleKey.RightArrow &&
                       button != ConsoleKey.DownArrow &&
                       button != ConsoleKey.UpArrow &&
                       button != ConsoleKey.PageUp &&
                       button != ConsoleKey.PageDown)
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
                        if (sure.AreUSure("", "Вы уверены, что хотите данный рейс?"))
                        {
                            Prices_List.Remove(Reises_List[line].P_ReisCode);
                            Reises_List.RemoveAt(line);

                            if (Reises_List.Count == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Список не заполнен!");
                                Console.ReadKey();
                                return;
                            }
                        }

                    } // удалить код рейса
                    else if (column == 1) // удалить аэропорт вылета
                    {
                        bool sure = false;
                        if (sure.AreUSure("", "Вы уверены, что хотите удалить выбранный элемент?"))
                        {
                            Reises_List[line].Airport_FlyOut.DeleteAirport(Reises_List[line].Airport_FlyOut);
                        }
                    } // удалить аэропорт вылета
                    else if (column == 2) // удалить аэропорт прилёта
                    {
                        bool sure = false;
                        if (sure.AreUSure("", "Вы уверены, что хотите удалить выбранный элемент?"))
                        {
                            Reises_List[line].Airport_FlyIn.DeleteAirport(Reises_List[line].Airport_FlyIn);
                        }
                    } // удалить аэропорт прилёта

                }
                else if (button == ConsoleKey.RightArrow)
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
                    if (column > 0)
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
                else if (button == ConsoleKey.PageUp)
                {
                    start_point -= end_point;
                    line = start_point;


                    if (start_point < 0)
                    {
                        line = Reises_List.Count - 1;
                        start_point = (Reises_List.Count - 1) - ((Reises_List.Count - 1) % end_point);
                    }

                }
                else if (button == ConsoleKey.PageDown)
                {
                    start_point += end_point;
                    line = start_point;

                    if (start_point >= Reises_List.Count)
                    {
                        line = 0;
                        start_point = 0;
                    }
                }

            }
        }

        #endregion
        // =============================================================================


            
            
        // =============================================================================
        #region РЕДАКТИРОВАНИЕ

        /// <summary>
        /// РЕДАКТИРОВАНИЕ
        /// </summary>
        /// <param name="airports"></param>
        /// <param name="prices"></param>
        public void RedactInfo(List<Price> Prices_List, List<Airport> Airports_List, List<Reis> Reises_List)
        {
            Airport airport = new Airport();


            bool exit = false;
            int line = 0;
            int column = 0;

            int start_point = 0;
            int end_point = 10;

            if (Reises_List.Count == 0)
            {
                Console.WriteLine("Список пуст. Нажмите любую кнопку. чтобы продолжить...");
                Console.ReadKey(true);
                return;
            }
                

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


                Console.WriteLine("╚═══════╩═══════════════════════════════════╩═══════════════════════════════════╩══════════════╩══════════════╩═════════════════════════════╝\n" +
                                  "ESC - выйти\n");

                var button = Console.ReadKey(true).Key;

                while (button != ConsoleKey.Escape &&
                       button != ConsoleKey.Enter &&
                       button != ConsoleKey.LeftArrow &&
                       button != ConsoleKey.RightArrow &&
                       button != ConsoleKey.DownArrow &&
                       button != ConsoleKey.UpArrow &&
                       button != ConsoleKey.PageUp &&
                       button != ConsoleKey.PageDown)
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
                            RedactCode(Prices_List, Reises_List[line].P_ReisCode);
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
                                    for (j = 0; j < Airports_List.Count; j++)
                                    {
                                        if (Airports_List[j] == Reises_List[line].Airport_FlyIn)
                                            break;
                                    }

                                    indexofAirport = airport.ChooseCreatedAirport(Airports_List, j);
                                    if (indexofAirport == -1)
                                        exxxit = true;
                                    else
                                    {
                                        Reises_List[line].Airport_FlyOut = Airports_List[indexofAirport];
                                        exxxit = true;
                                    }

                                }                                
                                else
                                { // изменил свойства и сделал ссылки
                                    RedactAirport(Reises_List[line].Airport_FlyOut, Reises_List[line].Airport_FlyIn, Airports_List);
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
                                    for (j = 0; j < Airports_List.Count; j++)
                                    {
                                        if (Airports_List[j] == Reises_List[line].Airport_FlyOut)
                                            break;
                                    }

                                    indexofAirport = airport.ChooseCreatedAirport(Airports_List, j);
                                    if (indexofAirport == -1)
                                        exxxit = true;
                                    else
                                    {
                                        Reises_List[line].Airport_FlyIn = Airports_List[indexofAirport];
                                        exxxit = true;
                                    }

                                }
                                else
                                {
                                    RedactAirport(Reises_List[line].Airport_FlyIn, Reises_List[line].Airport_FlyOut, Airports_List);
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
                else if (button == ConsoleKey.RightArrow)
                {
                    if (column < 6)
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
                    if (column > 0)
                    {
                        --column;
                    }
                    else
                    {
                        column = 5;
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
                else if (button == ConsoleKey.PageUp)
                {
                    start_point -= end_point;
                    line = start_point;


                    if (start_point < 0)
                    {
                        line = Reises_List.Count - 1;
                        start_point = (Reises_List.Count - 1) - ((Reises_List.Count - 1) % end_point);
                    }

                }
                else if (button == ConsoleKey.PageDown)
                {
                    start_point += end_point;
                    line = start_point;

                    if (start_point >= Reises_List.Count)
                    {
                        line = 0;
                        start_point = 0;
                    }
                }

            }
        }

        

        //===================================  КОД РЕЙСА  ===================================

        /// <summary>
        /// ИЗМЕНЕНИЕ КОДА РЕЙСА
        /// </summary>
        /// <param name="plist">список кодов рейсов</param>
        /// <param name="price">изменяемый код рейса</param>
        private void RedactCode(List<Price> Prices_List, Price price)
        {
            string reisCode = price.ReisCode;
            int BC_Num = price.BuisnessClass_Num;
            int BC_Price = price.BuisnessClass_Price;
            int EC_Num = price.EconomClass_Num;
            int EC_Price = price.EconomClass_Price;

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
                                    price.EditInfo(price, reisCode, BC_Price, BC_Num, EC_Price, EC_Num);
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

                                    RedactReisCode(Prices_List, ref reisCode, ref exit);                                
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
                                    price.EditInfo(price, reisCode, BC_Price, BC_Num, EC_Price, EC_Num);
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
                                    price.EditInfo(price, reisCode, BC_Price, BC_Num, EC_Price, EC_Num);
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
                                    price.EditInfo(price, reisCode, BC_Price, BC_Num, EC_Price, EC_Num);
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
                                    price.EditInfo(price, reisCode, BC_Price, BC_Num, EC_Price, EC_Num);
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
        /// РЕДАКТИРОВАНИЕ КОДА РЕЙСА
        /// </summary>
        /// <param name="reisCode">итоговый код</param>
        /// <param name="exit">условие выхода</param>
        private void RedactReisCode(List<Price> Prices_List, ref string reisCode, ref bool exit)
        {
            string promeghutochnoe;
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
            else // код рейса введён верно
            {
                promeghutochnoe = input;
                for (int i = 0; i < Prices_List.Count; i++)
                {
                    if (promeghutochnoe == Prices_List[i].ReisCode && reisCode != promeghutochnoe)
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

        //===================================  КОД РЕЙСА  ===================================




        //===================================  АЭРОПОРТ  ===================================

        /// <summary>
        /// Изменить только этот элемент или все?
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
                    #region ТОЛЬКО ЭТОТ
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

                    #region ВСЕ
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
        /// ИЗМЕНЕНИЕ ИНФОРМАЦИИ ОБ АЭРОПОРТЕ
        /// </summary>
        /// <param name="airport1">аэропорт1</param>
        /// <param name="airport2">аэропорт2</param>
        private void RedactAirport(Airport airport1, Airport airport2, List<Airport> Airports_List)
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
                                    airport1.EditInfo(airport1, Iata, City, Name);
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
                                        exit = isRedactedCodeCreated(Iata, airport1.Airport_Code, Airports_List);
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
                                    airport1.EditInfo(airport1, Iata, City, Name);
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
                                    airport1.EditInfo(airport1, Iata, City, Name);
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
        /// ПРОВЕРКА НА УНИКАЛЬНОСТЬ ИЗМЕНЁННОГО КОДА АЭРОПОРТА
        /// </summary>
        /// <param name="air_code">Изменённый код (можно ввести тот, что был до этого</param>
        /// <param name="not_redact">Изначальный код</param>
        /// <param name="Airports_List">список аэропортов</param>
        /// <returns></returns>
        private bool isRedactedCodeCreated(string air_code, string not_redact, List<Airport> Airports_List)
        {
            foreach (var air in Airports_List)
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

        //===================================  АЭРОПОРТ  ===================================

        
        //===================================  ДАТА ВЫЛЕТА/ПРИЛЁТА  ===================================

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

        //===================================  ДАТА ВЫЛЕТА/ПРИЛЁТА  ===================================
        
        
        //===================================  МАРКА САМОЛЁТА  ===================================

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

        //===================================  МАРКА САМОЛЁТА  ===================================


        // ~~~~~~~~~~~~~~~~~~~~~~~~~~  ЧТО ЭТО ТАКОЕ?!  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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

        #endregion
        // =============================================================================
        



        // =============================================================================
        #region ПОИСК
        
        public void SearchInfo(Airport airport, Price prices, List<Reis> Reises_List)
        {
            Console.Clear();

            bool exit = false;
            string input = "";
            int n = 0;
            string text = "   Поиск:";

            List<Reis> searchElements = new List<Reis>();


            if (Reises_List.Count == 0)
            {
                Console.WriteLine("Список пуст. Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
                return;
            }
            else
            {

                while (!exit)
                {
                    Console.Clear();

                    PrintReises(Reises_List, input, true, true);
                    Console.SetCursorPosition(0, 0);
                    
                        input = InputDeWay(text,ref exit);

                        searchElements.Clear();

                        foreach (var element in Reises_List)
                        {
                            if (
                                element.P_ReisCode.ReisCode.ToString().Contains(input) ||
                                element.Airport_FlyOut.Airport_City.Contains(input) ||
                                element.Airport_FlyIn.Airport_City.Contains(input) ||
                                element.Data_FlyOut.ToString().Contains(input) ||
                                element.Data_FlyIn.ToString().Contains(input) ||
                                element.PlaneMark.Contains(input)
                               )
                            {
                                searchElements.Add(element);
                            }
                        }

                        if (searchElements.Count == 0)
                        {
                            Console.WriteLine("\nСовпадений не найдено. Нажмите любую кнопку, чтобы продолжить...");
                            Console.ReadKey(true);
                            input = "";

                        }
                        else
                        {
                            PrintReises(searchElements, input, true);
                            input = "";
                        }
                  
                    
                }
                
                
            }
            return;




        }




        #endregion
        // =============================================================================

        
        
        // =============================================================================
        #region СОРТИРОВКА

        public void Sorting(ref List<Reis> Reises_List)
        {
            Airport airport = new Airport();
            Price price = new Price();
            bool exit = false;
            int start_point = 0;
            int end_point = 10;

            int column = 0;
            int lastChoice = 0;

            if (Reises_List.Count == 0)
            {
                Console.WriteLine("Список пуст. Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
                return;
            }

            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                                  "║                                                               Таблица Рейсов                                                              ║\n" +
                                  "║                                                                                                                                           ║\n" +
                                  "╠═══════╦═══════════════════════════════════╦═══════════════════════════════════╦══════════════╦══════════════╦═════════════════════════════╣");

                // выбор поля для сортировки
                if (column == 0)
                {
                    Console.Write("║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("  код  ");
                    Console.ResetColor();

                    Console.WriteLine("║          Аэропорт вылета          ║          Аэропорт прилёта         ║ Дата и время ║ Дата и время ║        Марка самолёта       ║");

                    Console.Write("║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" рейса ");
                    Console.ResetColor();

                    Console.WriteLine("║                                   ║                                   ║    вылета    ║    вылета    ║                             ║");

                }
                else if (column == 1)
                {
                    Console.Write("║  код  ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("          Аэропорт вылета          ");
                    Console.ResetColor();

                    Console.WriteLine("║          Аэропорт прилёта         ║ Дата и время ║ Дата и время ║        Марка самолёта       ║");

                    // 2 часть
                    Console.Write("║ рейса ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("                                   ");
                    Console.ResetColor();

                    Console.WriteLine("║                                   ║    вылета    ║    вылета    ║                             ║");

                }
                else if (column == 2)
                {
                    Console.Write("║  код  ║          Аэропорт вылета          ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("          Аэропорт прилёта         ");
                    Console.ResetColor();

                    Console.WriteLine("║ Дата и время ║ Дата и время ║        Марка самолёта       ║");

                    // 2 часть
                    Console.Write("║ рейса ║                                   ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("                                   ");
                    Console.ResetColor();

                    Console.WriteLine("║    вылета    ║    вылета    ║                             ║");

                }
                else if (column == 3)
                {
                    Console.Write("║  код  ║          Аэропорт вылета          ║          Аэропорт прилёта         ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" Дата и время ");
                    Console.ResetColor();

                    Console.WriteLine("║ Дата и время ║        Марка самолёта       ║");

                    // 2 часть
                    Console.Write("║ рейса ║                                   ║                                   ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("    вылета    ");
                    Console.ResetColor();

                    Console.WriteLine("║    вылета    ║                             ║");

                }
                else if (column == 4)
                {
                    Console.Write("║  код  ║          Аэропорт вылета          ║          Аэропорт прилёта         ║ Дата и время ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" Дата и время ");
                    Console.ResetColor();

                    Console.WriteLine("║        Марка самолёта       ║");

                    // 2 часть
                    Console.Write("║ рейса ║                                   ║                                   ║    вылета    ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("    вылета    ");
                    Console.ResetColor();

                    Console.WriteLine("║                             ║");
                }
                else if (column == 5)
                {
                    Console.Write("║  код  ║          Аэропорт вылета          ║          Аэропорт прилёта         ║ Дата и время ║ Дата и время ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("        Марка самолёта       ");
                    Console.ResetColor();

                    Console.WriteLine("║");

                    // 2 часть
                    Console.Write("║ рейса ║                                   ║                                   ║    вылета    ║    вылета    ║");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("                             ");
                    Console.ResetColor();

                    Console.WriteLine("║");
                }


                int i = start_point;
                while (((i + 1) % end_point > 0) && (i < Reises_List.Count))
                {
                    ReisTablBody(Reises_List[i].P_ReisCode,
                                 Reises_List[i].Airport_FlyOut,
                                 Reises_List[i].Airport_FlyIn,
                                 Reises_List[i].Data_FlyOut,
                                 Reises_List[i].Data_FlyIn,
                                 Reises_List[i].PlaneMark,
                                 ref i, column);
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
                                 ref i, column);
                }

                Console.WriteLine("╚═══════╩═══════════════════════════════════╩═══════════════════════════════════╩══════════════╩══════════════╩═════════════════════════════╝\n" +
                                  "ESC - выйти\n");


                var button = Console.ReadKey(true).Key;
                Console.WriteLine(button.GetType());
                while (button != ConsoleKey.Escape &&
                       button != ConsoleKey.Enter &&
                       button != ConsoleKey.LeftArrow &&
                       button != ConsoleKey.RightArrow)
                {
                    button = Console.ReadKey(true).Key;
                }

                if (button == ConsoleKey.Escape)
                    return;
                else if (button == ConsoleKey.Enter)
                {
                    switch (column)
                    {
                        case 0:
                            if (lastChoice == column)
                                Reises_List.Reverse();
                            else
                                Reises_List = Reises_List.OrderBy(e => e.P_ReisCode.ReisCode).ToList();

                            PrintReises(Reises_List);

                            break;
                        case 1:
                            if (lastChoice == column)
                                Reises_List.Reverse();
                            else
                                Reises_List = Reises_List.OrderBy(e => e.Airport_FlyOut.Airport_Name).ToList();

                            PrintReises(Reises_List);

                            break;
                        case 2:
                            if (lastChoice == column)
                                Reises_List.Reverse();
                            else
                                Reises_List = Reises_List.OrderBy(e => e.Airport_FlyIn.Airport_Name).ToList();

                            PrintReises(Reises_List);

                            break;
                        case 3:
                            if (lastChoice == column)
                                Reises_List.Reverse();
                            else
                                Reises_List = Reises_List.OrderBy(e => e.Data_FlyOut).ToList();
                            PrintReises(Reises_List);

                            break;
                        case 4:
                            if (lastChoice == column)
                                Reises_List.Reverse();
                            else
                                Reises_List = Reises_List.OrderBy(e => e.Data_FlyIn).ToList();

                            PrintReises(Reises_List);

                            break;
                        case 5:
                            if (lastChoice == column)
                                Reises_List.Reverse();
                            else
                                Reises_List = Reises_List.OrderBy(e => e.PlaneMark).ToList();

                            PrintReises(Reises_List);

                            break;
                    }

                    lastChoice = column;

                }
                else if (button == ConsoleKey.RightArrow)
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
                else if (button == ConsoleKey.LeftArrow)
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


            }
        }

        #endregion
        // =============================================================================




        // =============================================================================
        #region СОХРАНЕНИЕ
        
        public void Saving(List<Price> Prices_List, List<Airport> Airports_List, List<Reis> Reises_List)
        {
            bool isEscape = false;
            string text = "Введите путь к файлу:";
            try
            {
                string file = InputDeWay(text, ref isEscape);

                if (isEscape)
                    return;

                using (StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8))
                {
                    foreach (var airport in Airports_List)
                    {
                        sw.WriteLine($"{airport.Airport_Code}|{airport.Airport_City}|{airport.Airport_Name}");
                    }

                    sw.WriteLine("~");

                    foreach (var price in Prices_List)
                    {
                        sw.WriteLine($"{price.ReisCode}|{price.BuisnessClass_Num}|{price.BuisnessClass_Price}|{price.EconomClass_Num}|{price.EconomClass_Price}");
                    }

                    sw.WriteLine("~");

                    foreach (var reis in Reises_List)
                    {
                        sw.WriteLine($"{Prices_List.IndexOf(reis.P_ReisCode)}|" +
                                     $"{Airports_List.IndexOf(reis.Airport_FlyOut)}|" +
                                     $"{Airports_List.IndexOf(reis.Airport_FlyIn)}|" +
                                     $"{reis.Data_FlyOut}|" +
                                     $"{reis.Data_FlyIn}|" +
                                     $"{reis.PlaneMark}");
                    }

                    sw.WriteLine("~");
                }
            }
            catch (ArgumentNullException)
            {
                Console.Clear();
                Console.WriteLine("Вы ничего не ввели.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (FileNotFoundException)
            {
                Console.Clear();
                Console.WriteLine("Файл не найден.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (IndexOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Ошибка в структуре файла.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (UnauthorizedAccessException)
            {
                Console.Clear();
                Console.WriteLine("Отказано в доступе.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (ArgumentException)
            {
                Console.Clear();
                Console.WriteLine("Вы ничего не ввели.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }

        }



        #endregion
        // =============================================================================




        // =============================================================================
        #region ЗАГРУЗКА

        public void Loading(ref List<Price> Prices_List, ref List<Airport> Airports_List, ref List<Reis> Reises_List)
        {
            bool isEscape = false;
            string text = "Введите путь к файлу:";
            try
            {
                string file = InputDeWay(text, ref isEscape);

                if (isEscape)
                    return;

                List<Airport> airportsList = new List<Airport>();
                List<Price> pricesList = new List<Price>();
                List<Reis> reisesList = new List<Reis>();


                using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
                {

                    // Airports_List
                    for (string input = sr.ReadLine(); input != "~"; input = sr.ReadLine())
                    {
                        Airport airport = new Airport();

                        var ex = input.Split('|');

                        airport.EditInfo(airport, ex[0], ex[1], ex[2]);

                        airportsList.Add(airport);
                    }

                    // Prices_List
                    for (string input = sr.ReadLine(); input != "~"; input = sr.ReadLine())
                    {
                        Price price = new Price();

                        var ex = input.Split('|');

                        price.EditInfo(price, ex[0], int.Parse(ex[2]), int.Parse(ex[1]), int.Parse(ex[4]), int.Parse(ex[3]));

                        pricesList.Add(price);
                    }

                    // Reises_List
                    for (string input = sr.ReadLine(); input != "~"; input = sr.ReadLine())
                    {
                        Reis reis = new Reis();

                        var ex = input.Split('|');

                        reis.P_ReisCode = pricesList[int.Parse(ex[0])];
                        reis.Airport_FlyOut = airportsList[int.Parse(ex[1])];
                        reis.Airport_FlyIn = airportsList[int.Parse(ex[2])];
                        reis.Data_FlyOut = DateTime.ParseExact(ex[3], "d.M.yyyy H:m:s", CultureInfo.CurrentCulture);
                        reis.Data_FlyIn = DateTime.ParseExact(ex[4], "d.M.yyyy H:m:s", CultureInfo.CurrentCulture);
                        reis.PlaneMark = ex[5];

                        reisesList.Add(reis);
                    }

                    Airports_List = airportsList;
                    Prices_List = pricesList;
                    Reises_List = reisesList;
                    
                }

            }
            catch (ArgumentNullException)
            {
                Console.Clear();
                Console.WriteLine("Вы ничего не ввели.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (FileNotFoundException)
            {
                Console.Clear();
                Console.WriteLine("Файл не найден.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (IndexOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Ошибка в структуре файла.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (UnauthorizedAccessException)
            {
                Console.Clear();
                Console.WriteLine("Отказано в доступе.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }
            catch (ArgumentException)
            {
                Console.Clear();
                Console.WriteLine("Вы ничего не ввели.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey(true);
            }

        }

        #endregion
        // =============================================================================




        public override string ToString() => Airport_FlyOut.Airport_City;


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


        /// <summary>
        /// Ввод пути к файлу
        /// </summary>
        /// <param name="isEscape">условие выхода при нажатии на Escape</param>
        /// <returns></returns>
        private string InputDeWay(string text, ref bool isEscape)
        {
            string input = ""; //  путь к файлу
            char c; // символ, который вводится

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(text);
            Console.ResetColor();
            Console.Write(" " + input);

            var button = Console.ReadKey(true);


            while (button.Key != ConsoleKey.Escape && button.Key != ConsoleKey.Enter)
            {

                if (button.Key == ConsoleKey.Backspace)
                {
                    if (input.Length != 0)
                        input = input.Substring(0, input.Length - 1);
                }
                else
                {
                    if (input.Length <= 60)
                    {
                        c = button.KeyChar;
                        if (char.IsDigit(c) || char.IsLetter(c) || char.IsPunctuation(c))
                            input += c;
                    }
                    
                }

                Console.SetCursorPosition(text.Length+1, 0);
                Console.Write(new string(' ', input.Length + 20));
                Console.SetCursorPosition(text.Length+1, 0);

                Console.Write(" " + input);

                button = Console.ReadKey(true);
            }

            if (button.Key == ConsoleKey.Escape)
            {
                input = "";
                isEscape = true;
            }

            return input;
        }



    }
}
