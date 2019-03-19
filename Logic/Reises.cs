using System;
using System.Collections.Generic;
using System.Globalization;
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


            DateTime FlyOut_data = new DateTime();
            DateTime FlyIn_data = new DateTime();
            string planemark="";
            bool rightData = false;
            bool allright = false, exit = false;


            while (!allright)
            {
                // аэропорт вылета
                while (!exit)
                {
                    if (NewOrCreated("---> Добавление Рейса <---\n\n\n\n\n\n\nАэропорт вылета: ", ref exit))
                    {
                        airports.Airport_Adding(); // предпоследний в списке (predposl)
                    }
                    else
                    {
                        // ТАБЛИЦА СУЩЕСТВУЮЩИХ АЭРОПОРТОВ
                        exit = true;
                    }

                }
                exit = false;

                // аэропорт прилёта
                while (!exit)
                {
                    if (NewOrCreated("---> Добавление Рейса <---\n" +
                                     "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 1].Airport_Name + "\n\n\n\n\n" +
                                     "Аэропорт прилёта: ", ref exit))
                    {
                        airports.Airport_Adding(); // предпоследний в списке (predposl)
                    }
                    else
                    {
                        // ТАБЛИЦА СУЩЕСТВУЮЩИХ АЭРОПОРТОВ
                        exit = true;
                    }

                }
                exit = false;



                // код рейса + цена + места
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 2].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List.Last().Airport_Name + "\n\n\n\n");

                    Console.Write("Ввод кода рейса...");
                    Console.ReadKey(true);
                    Console.Clear();
                    prices.AddPriceForReis(); // последний в списке
                    exit = true;

                }
                exit = false;




                // дата вылета
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 2].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List.Last().Airport_Name + "\n" +
                                      "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n\n\n");
                    try
                    {
                        Console.WriteLine("Дата вылета.");
                        exit = isCorrect_Date(out FlyOut_data);

                    }
                    catch
                    {
                        
                    }
                    
                }
                exit = false;

                // дата прилёта
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 1].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List.Last().Airport_Name + "\n" +
                                      "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n" +
                                      $"Дата вылета: {FlyOut_data.Day}.{FlyOut_data.Month}.{FlyOut_data.Year}   {FlyOut_data.Hour}:{FlyOut_data.Minute}\n\n");


                    try
                    {
                        Console.WriteLine("Дата прилёта.");
                        exit = isCorrect_Date(out FlyIn_data);
                        exit = isCorrect_Difference(FlyOut_data, FlyIn_data);
                        
                    }
                    catch
                    {

                        Console.WriteLine("\n\nДанные введены не по образцу. Повторите попытку.\n" +
                                          "Нажмите любую клавишу, чтобы продолжить...");
                        Console.ReadKey(true);

                    }

                }
                exit = false;



                
                // марка самолёта
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("---> Добавление Рейса <---\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 2].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List.Last().Airport_Name + "\n" +
                                      "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n" +
                                      $"Дата вылета: {FlyOut_data.Day}.{FlyOut_data.Month}.{FlyOut_data.Year}   {FlyOut_data.Hour}:{FlyOut_data.Minute}\n" +
                                      $"Дата прилёта: {FlyIn_data.Day}.{FlyIn_data.Month}.{FlyIn_data.Year}   {FlyIn_data.Hour}:{FlyIn_data.Minute}\n");

                    Console.Write("Марка самолёта: ");
                    exit = isCorrect_String(out planemark);

                }

                //-----------------------------------------------------------------------------------------------------------------------------------------------

                // подтверждение ввода
                bool apply = false;
                while (!apply) // цикл отображения таблички
                {
                    Console.Clear();
                    Console.WriteLine();

                    Airports.AreUSure("---> Добавление Рейса <---\n" +
                                      "Аэропорт вылета: " + airports.Airport_List[airports.Airport_List.Count - 2].Airport_Name + "\n" +
                                      "Аэропорт прилёта: " + airports.Airport_List.Last().Airport_Name + "\n" +
                                      "Код рейса: " + prices.Prices_List.Last().ReisCode + "\n" +
                                      $"Дата вылета: {FlyOut_data.Day}.{FlyOut_data.Month}.{FlyOut_data.Year}   {FlyOut_data.Hour}:{FlyOut_data.Minute}\n" +
                                      $"Дата прилёта: {FlyIn_data.Day}.{FlyIn_data.Month}.{FlyIn_data.Year}   {FlyIn_data.Hour}:{FlyIn_data.Minute}\n" +
                                      "Марка самолёта: " + planemark,
                                      ref allright, ref apply);
                }

            }
            exit = false;


            Console.WriteLine("ВСЁ ЗАПОЛНЕНО УРЯЯЯЯ");

            Reises_List.Add(new Reises { Airport_FlyOut = airports.Airport_List[airports.Airport_List.Count - 2]
                                        ,Airport_FlyIn = airports.Airport_List.Last()
                                        ,Data_FlyOut = FlyOut_data
                                        ,Data_FlyIn = FlyIn_data
                                        ,PlaneMark = planemark});

             Console.ReadKey(true);

        }


        // Добавить новый аэропорт или ужу созданный?
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
                            smallExit = true; // выход из 'малого' цикла
                            return false;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }

            }

        }



        // правильная дата (~2 года)
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



        // проверка, что время полёта >18
        private static bool isCorrect_Difference(DateTime FlyOut_date, DateTime FlyIn_date)
        {
            if (FlyIn_date.Date < FlyOut_date.Date)
            {
                Console.WriteLine("\nДата прилёта не может быть раньше даты вылета. Заполните поле снова.\n" +
                                  "Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (FlyIn_date.Hour <= FlyOut_date.Hour /*&& !(FlyIn_date.Date < FlyOut_date.Date)*/)
            {
                Console.WriteLine("\nВремя прилёта не может быть раньше времени вылета. Заполните поле снова.\n" +
                                  "Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else
            {
                var difference = FlyIn_date - FlyOut_date;
                DateTime dima = new DateTime(difference.Ticks);

                int hour = dima.Hour;

                if (hour > 0 && hour <= 18)
                    return true;
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
        /// проверка введённых данных на корректность
        /// </summary>
        /// <param name="testingName">строка для проверки</param>
        /// <param name="minLenght">минимальная длина</param>
        /// <param name="maxLenght">максимальная длина</param>
        /// <returns></returns>
        public static bool isCorrect_String(out string testingName, int minLenght = 3, int maxLenght = 30)
        {
            testingName = Console.ReadLine(); // чтение введённой сроки
            testingName = testingName.Trim(); // удаление ненужных пробелов в начале и конце введённой строки

            if (string.IsNullOrWhiteSpace(testingName)) // пустая
            {
                Console.WriteLine("\n\nПоле не заполнено! Заполните поле.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (!IsIllegalSymb(testingName)) // есть ли запрещённые символы
            {
                Console.WriteLine("\n\nВведённая строка содержит запрещённые символы. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingName.Length < minLenght) // меньше минимальной длины
            {
                Console.WriteLine("\n\nВведённая строка слишком короткая. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingName.Length > maxLenght) // больше максимальной длины
            {
                Console.WriteLine("\n\nВведённая строка слишком длинная. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else // все проверки пройдены успешно
                return true;

        }

        /// <summary>
        /// проверка на запрещённые символы
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private static bool IsIllegalSymb(string test) // передача строки в метод
        {
            string illegalChars = "!@\"#$%^&*_+=;<>!?\\/|[]{}"; // "запрещённые" символы

            foreach (char c in test) // берём символ из строки
            {
                foreach (char illegal in illegalChars) // берём символ из "запрещённых" символов
                {
                    if (c == illegal) // сравниваем
                        return false; // возвращает false, если в строке есть "запрещённый" символ
                }
         
            }
            return true; // возвращает true, если "запрещённых" символов нет
        }



    }
}
