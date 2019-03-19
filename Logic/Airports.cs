using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace Logic
{
    public class Airports
    {
        /// <summary>
        /// Код аэропорта
        /// </summary>
        public string Airport_Code { get; set; }

        /// <summary>
        /// Город аэропорта (где расположен)
        /// </summary>
        public string Airport_City { get; set; }

        /// <summary>
        /// Название аэропорта
        /// </summary>
        public string Airport_Name { get; set; }

        /// <summary>
        /// список аэропортов
        /// </summary>
        public List<Airports> Airport_List = new List<Airports>();

        /// <summary>
        /// Метод добавления аэропорта
        /// </summary>
        public void Airport_Adding()
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

                    exit = isCorrect(out code, 3, 3); // проверка на корректность
                    code = code.ToUpper(); // перевод в верхний регистр

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
                    exit = isCorrect(out city, 3, 50); // проверка на корректность
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
                    exit = isCorrect(out name, 3, 60); // проверка на корректность
                }
                exit = false; // обновление условия цикла
                #endregion

                //------------------------------

                #region Подтверждение введённых данных
                bool apply = false;
                while (!apply) // цикл отображения таблички
                {
                    Console.Clear();
                    Console.WriteLine();

                    AreUSure("---> Добавление Аэропорта <---\n" +
                             "\nIATA код аэропорта: " + code +
                             "\nГород аэропорта: " + city +
                             "\nНазвание аэропорта: " + name + "\n\n", 
                             ref allright, ref apply);
                }
                #endregion

            }
            // добавление нового аэропорта в список 
            Airport_List.Add(new Airports { Airport_Code = code, Airport_City = city, Airport_Name = name });
        }


        // isCorrect
        #region Проверка введённой строки на корректность

        /// <summary>
        /// проверка введённых данных на корректность
        /// </summary>
        /// <param name="testingName">строка для проверки</param>
        /// <param name="minLenght">минимальная длина</param>
        /// <param name="maxLenght">максимальная длина</param>
        /// <returns></returns>
        public static bool isCorrect(out string testingName, int minLenght=3, int maxLenght = 60)
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

        #endregion



        // isIllegalSymbol
        #region Метод проверки на наличие "запрещённых символов" в строке
        private static bool IsIllegalSymb(string test) // передача строки в метод
        {
            string illegalChars = "1234567890!@\"#$%^&*_+=:;<>!?\\/|[]{}\t"; // "запрещённые" символы

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
        #endregion

        // AreUSure
        #region Вы уверены?

        /// <summary>
        /// Вы уверены, что всё ввели верно?
        /// </summary>
        /// <param name="info">выводит переданный текст</param>
        /// <param name="bigExit">условие выхода из цикла, который отвечает за изменение отображения таблички</param>
        /// <param name="smallExit">условие выхода из цикла, ответственного за добавление элемента</param>
        public static void AreUSure(string info, ref bool bigExit, ref bool smallExit)
        {
            // выбор (от него зависит, что выведет)
            byte myChoise = 1;
            while (true) // цикл отображения ДА / НЕТ
            {
                Console.Clear();
                Console.WriteLine(info);
                Console.WriteLine("Вы уверены, что всё ввели верно?");
                switch (myChoise) // что вывести в зависимости от значения myChoise
                {
                    #region ДА
                    case 1: //------------------------------------------------ выделяем вариант ДА
                        Console.Write("   ");
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(" ДА ");
                        Console.ResetColor();
                        Console.WriteLine(" |  НЕТ"); //---------------------- всё покрасилось красиво

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
                            bigExit = true; // выход из 'большого' цикла
                            smallExit = true; // выход из 'малого' цикла
                            return;
                        }
                        break;
                    #endregion

                    #region НЕТ
                    case 2:
                        Console.Write("    ДА  | ");//------------------------ выделяем вариант НЕТ
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" НЕТ ");
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
                            bigExit = false; // выход из 'большого' цикла
                            smallExit = true; // выход из 'малого' цикла
                            return;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }

            }

        }
        #endregion





        public void PrintAirports(Airports airportFlyOut, Airports airportFlyIn)
        {
            //   ╔ ╦ ╗ ╠ ╬ ╣ ╚ ╩ ╝ ═ ║

            int city_d = 50;
            int name_d = 60;


            Console.WriteLine(" ╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                              " ║                                                      Таблица аэропортов                                                       ║\n" +
                              " ╠═════╦═══════════════════════════════════════════════════════╦═════════════════════════════════════════════════════════════════╣\n" +
                              " ║ Код ║                        Город                          ║                              Название                           ║\n" +
                              " ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣" ); 
            
            Console.WriteLine($" ║ {airportFlyOut.Airport_Code} ║   {airportFlyOut.Airport_City,50}  ║   {airportFlyOut.Airport_Name,60}  ║" +
                               " ╠═════╬═══════════════════════════════════════════════════════╬═════════════════════════════════════════════════════════════════╣\n ");

            Console.WriteLine($" ║ {airportFlyIn.Airport_Code} ║   {airportFlyIn.Airport_City,50}  ║   {airportFlyIn.Airport_Name,60}  ║" +
                               " ╚═════╩═══════════════════════════════════════════════════════╩═════════════════════════════════════════════════════════════════╝ ");

            var press = Console.ReadKey(true).Key;
            while (press != ConsoleKey.Escape)
            {
                press = Console.ReadKey(true).Key;
            }















        }




    }
}
