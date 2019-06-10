using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace Logic
{
    public class Airport
    {
        /// <summary>
        /// Код аэропорта
        /// </summary>
        public string Airport_Code { private set; get; }

        /// <summary>
        /// Город аэропорта (где расположен)
        /// </summary>
        public string Airport_City { private set; get; }

        /// <summary>
        /// Название аэропорта
        /// </summary>
        public string Airport_Name { private set; get; }
        

        /// <summary>
        /// Метод добавления аэропорта
        /// </summary>
        public void Airport_Adding(ref List<Airport> Airport_List)
        {
            // код, город, название аэропорта
            string code = "", city = "", name = "";

            // условие выхода из цикла
            bool exit = false, allright = false;

            while (!allright)
            {
                
                #region Добавление кода аэропорта

                // вход в цикл, который проверяет корректно ли введены данные кода аэропорта
                while (!exit)
                {
                    Console.Clear(); // очистка консоли

                    Console.WriteLine("---> Добавление Аэропорта <---\n\n\n\n\n"); // вывод текста

                    Console.Write("Введите IATA код аэропорта: "); // что нужно ввести

                    code = Console.ReadLine(); // ввода кода аэропорта
                    exit = code.isCorrectIATA(3, 3); // проверка на корректность

                    code = code.ToUpper(); // перевод в верхний регистр
                    exit = exit && AirportCodeAlreadyCreated(code, Airport_List);

                }
                exit = false; // обновление условия цикла
                #endregion

                #region Добавление города аэропорта

                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление Аэропорта <---\n"); // вывод текста
                    Console.WriteLine("IATA код аэропорта: " + code + "\n\n\n"); // что нужно ввести

                    Console.Write("Введите город аэропорта: ");

                    city = Console.ReadLine(); // ввод города аэропорта
                    city = city.Trim();
                    exit = city.isCorrectString(3, 50); // проверка на корректность
                }
                exit = false; // обновление условия цикла
                #endregion

                #region Добавление названия аэропорта

                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление Аэропорта <---\n"); // вывод текста
                    Console.WriteLine("IATA код аэропорта: " + code); // что нужно ввести
                    Console.WriteLine("Город аэропорта: " + city + "\n\n");

                    Console.Write("Введите название аэропорта: ");

                    name = Console.ReadLine(); // ввод названия аэропорта
                    name = name.Trim();
                    exit = name.isCorrectString(3, 60); // проверка на корректность
                }
                exit = false; // обновление условия цикла
                #endregion

                //------------------------------

                #region Подтверждение введённых данных

                Console.Clear();
                Console.WriteLine();

                allright = allright.AreUSure("---> Добавление Аэропорта <---\n" +
                                             "\nIATA код аэропорта: " + code +
                                             "\nГород аэропорта: " + city +
                                             "\nНазвание аэропорта: " + name + "\n\n");
                
                #endregion

            }
            // добавление нового аэропорта в список 
            Airport_List.Add(new Airport {
                                            Airport_Code = code,
                                            Airport_City = city,
                                            Airport_Name = name
                                          });
        }


        


        #region Аэропорт с таким кодом уже создан или нет
        /// <summary>
        /// есть ли такой код аэропорта
        /// </summary>
        /// <param name="air_code">код аэропорта</param>
        /// <param name="Airports_List">список аэропортов</param>
        /// <returns></returns>
        public bool AirportCodeAlreadyCreated(string air_code, List<Airport> Airports_List)
        {
            foreach (var air in Airports_List)
            {
                if (air.Airport_Code == air_code)
                {
                    Console.WriteLine("\n\nАэропорт с таким кодом уже существует. Введите новый код.\n" +
                                      "Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey(true);
                    return false;
                }
            }
            return true;
        }
        #endregion


        // вывод информации об аэропортах (вылета | прилёта)
        public void PrintAirports(Airport airportFlyOut, Airport airportFlyIn) 
        {
            //   ╔ ╦ ╗ ╠ ╬ ╣ ╚ ╩ ╝ ═ ║

            int city_d = 50;
            int name_d = 60;


            Console.WriteLine(" ╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                              " ║                                                      Таблица аэропортов                                                       ║\n" +
                              " ╠═════╦═══════════════════════════════════════════════════════╦═════════════════════════════════════════════════════════════════╣\n" +
                              " ║ Код ║                        Город                          ║                              Название                           ║\n" +
                              " ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣" );

            city_d -= airportFlyOut.Airport_City.Length;
            name_d -= airportFlyOut.Airport_Name.Length;
         
            Console.WriteLine($" ║ {airportFlyOut.Airport_Code} ║   {airportFlyOut.Airport_City}{new string(' ', city_d)}  ║   {airportFlyOut.Airport_Name}{new string(' ',name_d)}  ║\n" +
                               " ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣");

            city_d = 50;
            name_d = 60;

            city_d -= airportFlyIn.Airport_City.Length;
            name_d -= airportFlyIn.Airport_Name.Length;

            Console.WriteLine($" ║ {airportFlyIn.Airport_Code} ║   {airportFlyIn.Airport_City}{new string(' ', city_d)}  ║   {airportFlyIn.Airport_Name}{new string(' ', name_d)}  ║\n" +
                               " ╚═════╩═══════════════════════════════════════════════════════╩═════════════════════════════════════════════════════════════════╝ ");

            var press = Console.ReadKey(true).Key;
            while (press != ConsoleKey.Escape)
            {
                press = Console.ReadKey(true).Key;
            }
         }

        
        /// <summary>
        /// Выбор созданного аэропорта
        /// </summary>
        /// <param name="FlyOut_ind">индекс аэропорта вылета</param>
        /// <returns></returns>
        public int ChooseCreatedAirport( List<Airport> Airport_List, int FlyOut_ind = -1)
        {
            int chosenAirport = 0; // индекс выбранного аэропорта 

            int city_d = 50; // длина строки названия города
            int name_d = 60; // длина строки названия аэропорта

            int start_point = 0; // с какого индекса выводить элементы
            int end_point = 15; // сколько элементов можно вывести за раз

            while (true)
            {
                Console.Clear();

                if(chosenAirport == FlyOut_ind) 
                {
                    if (FlyOut_ind == 0) // если уже выбран 1-й аэропорт в списке,
                        chosenAirport = 1; // то переходим ко 2-му
                    else if (FlyOut_ind == Airport_List.Count - 1) // если уже выбран последний аэропорт в списке,
                        chosenAirport = FlyOut_ind - 1; // то переходим к предпоследнему
                }
                // отрисовка списка аэропортов
                Console.WriteLine(" ╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                                  " ║                                                      Таблица аэропортов                                                       ║\n" +
                                  " ╠═════╦═══════════════════════════════════════════════════════╦═════════════════════════════════════════════════════════════════╣\n" +
                                  " ║ Код ║                        Город                          ║                              Название                           ║");

                int i = start_point; // начало отчёта = start_point
                while (((i + 1) % end_point > 0) && (i < Airport_List.Count)) // вывод списка аэропортов
                { // т.к. отчёт идёт с 0, а 0 % n = 0, что противоречит 1-му условию цикла, то к i нужно прибавлять 1 (i+1)
                    
                    city_d = 50 - Airport_List[i].Airport_City.Length;
                    name_d = 60 - Airport_List[i].Airport_Name.Length;


                    if (FlyOut_ind != -1 && FlyOut_ind == i) // закрасить выбранный ранее аэропорт в СИНИЙ

                    { 

                        Console.WriteLine(" ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣");

                        Console.Write(" ║ ");
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write($"{ Airport_List[i].Airport_Code} ║   {Airport_List[i].Airport_City}{new string(' ', city_d)}  ║   {Airport_List[i].Airport_Name}{new string(' ', name_d)}  ");
                        Console.ResetColor();

                        Console.WriteLine("║");

                    }
                    else if (chosenAirport == i) // выделение аэропортов, по которым скачет пользователь (ЖЁЛТЫЙ)

                    {
                        Console.WriteLine(" ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣");

                        Console.Write(" ║ ");
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{ Airport_List[i].Airport_Code} ║   {Airport_List[i].Airport_City}{new string(' ', city_d)}  ║   {Airport_List[i].Airport_Name}{new string(' ', name_d)}  ");
                        Console.ResetColor();

                        Console.WriteLine("║");

                    }
                    else // не выбранные аэропорты (не закрашиваются)
                    {
                        Console.WriteLine(" ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣\n" +
                                     $" ║ {Airport_List[i].Airport_Code} ║   {Airport_List[i].Airport_City}{new string(' ', city_d)}  ║   {Airport_List[i].Airport_Name}{new string(' ', name_d)}  ║");
                    }

                    i++;

                }

                // из-за того, что i+1 мы не выведем последний элемент, 
                //кратный end_point. Для этого нужно уменьшить i на 1
                if ((i + 1) % end_point == 0 && (i < Airport_List.Count))
                {
                    city_d = 50 - Airport_List[i].Airport_City.Length;
                    name_d = 60 - Airport_List[i].Airport_Name.Length;


                    if (FlyOut_ind != -1 && FlyOut_ind == i)
                    {
                        Console.WriteLine(" ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣");

                        Console.Write(" ║ ");
                        // закрасить выбранный ранее аэропорт в СИНИЙ
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write($"{ Airport_List[i].Airport_Code} ║   {Airport_List[i].Airport_City}{new string(' ', city_d)}  ║   {Airport_List[i].Airport_Name}{new string(' ', name_d)}  ");
                        Console.ResetColor();

                        Console.WriteLine("║");

                    }
                    else if (chosenAirport == i)
                    {
                        Console.WriteLine(" ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣");

                        Console.Write(" ║ ");
                        // выделение аэропортов, по которым скачет пользователь (ЖЁЛТЫЙ)
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{ Airport_List[i].Airport_Code} ║   {Airport_List[i].Airport_City}{new string(' ', city_d)}  ║   {Airport_List[i].Airport_Name}{new string(' ', name_d)}  ");
                        Console.ResetColor();

                        Console.WriteLine("║");

                    }
                    else // не выбранные аэропорта (не закрашиваются)
                    {
                        Console.WriteLine(" ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣\n" +
                                     $" ║ {Airport_List[i].Airport_Code} ║   {Airport_List[i].Airport_City}{new string(' ', city_d)}  ║   {Airport_List[i].Airport_Name}{new string(' ', name_d)}  ║");

                    }

                    i++;
                }

                Console.WriteLine(" ╚═════╩═══════════════════════════════════════════════════════╩═════════════════════════════════════════════════════════════════╝\n" +
                                  "ESC - выйти");
                // закрытие вывода

                var button = Console.ReadKey().Key; // нажатая клавиша
                while(button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
                {
                    button = Console.ReadKey().Key; // если не разрешена, то нажимать ещё
                }
                switch (button)
                {
                    case ConsoleKey.Enter: // выбор
                        return chosenAirport;
                        break;

                    case ConsoleKey.Escape: // выход
                        return -1;
                        break;

                    case ConsoleKey.DownArrow: // вниз
                        ++chosenAirport;

                        if ((chosenAirport % end_point == 0) && (chosenAirport < Airport_List.Count))
                        {
                            start_point += end_point;
                        }

                        if (chosenAirport == Airport_List.Count)
                        {
                            chosenAirport = 0;
                            start_point = 0;
                        }

                        if (chosenAirport == FlyOut_ind && FlyOut_ind != Airport_List.Count-1)
                        {
                            ++chosenAirport;
                        }
                        else if (chosenAirport == FlyOut_ind && FlyOut_ind == Airport_List.Count-1)
                        {
                            chosenAirport = 0;
                            start_point = 0;
                        }
                           
                        break;

                    case ConsoleKey.UpArrow: // вверх
                        --chosenAirport; // перемещаем линию выделения на 1 вверх

                        if (chosenAirport < 0) // оказались позади 1 элемента
                        {
                            chosenAirport = Airport_List.Count - 1; // выделяем последний элемент списка аэропортов
                            start_point = (Airport_List.Count - 1) - ((Airport_List.Count - 1) % end_point); // высчитываем начало отрисовки таблицы

                            if (chosenAirport == FlyOut_ind && FlyOut_ind == Airport_List.Count - 1) // аэропорт вылета - последний в списке аэропортов
                            {
                                chosenAirport = Airport_List.Count - 2; // перемещаем линию выделения на предпоследний элемент
                            }
                        }
                        else if ((chosenAirport + 1) % end_point == 0) 
                        {
                            start_point -= end_point;
                        }

                        if (chosenAirport == FlyOut_ind && FlyOut_ind != 0) // линия перехода перешла на аэропорт вылета и он не первый в списке
                        {
                            --chosenAirport; // переходим на предыдущий элемент 
                            start_point -= end_point; // уменьшаем startpoint
                        }
                        else if (chosenAirport == FlyOut_ind && FlyOut_ind == 0) // линия перехода перешла на аэропорт вылета и он первый в списке
                        {
                            start_point = (Airport_List.Count - 1) - ((Airport_List.Count - 1) % end_point); // вы
                            chosenAirport = Airport_List.Count - 1;
                        }


                        break;

                    default:
                        break;
                }


            }
            

        }



        public void DeleteAirport(Airport airport)
        {
            airport.Airport_Code = "---";
            airport.Airport_City = "---";
            airport.Airport_Name = "---";
        }


        public void EditInfo(Airport airport, string Code, string City, string Name)
        {
            airport.Airport_Code = Code;
            airport.Airport_City = City;
            airport.Airport_Name = Name;
        }
    }
}
