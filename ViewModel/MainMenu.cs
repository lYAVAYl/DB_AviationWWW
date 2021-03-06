﻿//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Logic;


namespace ViewModel
{
    public class MainMenu //   ╔ ╦ ╗ ╠ ╬ ╣ ╚ ╩ ╝ ═ ║
    {

        Airport airport = new Airport();
        List<Airport> Airport_List = new List<Airport>();

        Price prices = new Price();
        List<Price> Prices_List = new List<Price>();

        Reis reis = new Reis();
        List<Reis> Reises_List = new List<Reis>();

        /*
           "1. Ввод данных \n" +++
           "2. Вывод таблицы на экран \n" +++
           "3. Удаление \n" +-
           "4. Редактировать \n" +++
           "5. Поиск \n" ---
           "6. Сортировка \n" ---
           "7. Сохранение \n" ---
           "8. Загрузка \n" ---
           "9. Выход" +++
        */


        // Вывод пунктов меню
        #region Главное меню
        int MyChoise { get; set; }

        /// <summary>
        /// Меню
        /// </summary>
        public void Menu()
        {
            MyChoise = 1; // изначальный выбор
            bool exitFromProgram = false; // условие выхода

            while (!exitFromProgram)
            {
                Console.Clear();

                // какой пункт меню выбран
                switch (MyChoise)
                {
                    case 1:
                        One();
                        break;

                    case 2:
                        Two();
                        break;

                    case 3:
                        Three();
                        break;

                    case 4:
                        Four();
                        break;

                    case 5:
                        Five();
                        break;

                    case 6:
                        Six();
                        break;

                    case 7:
                        Seven();
                        break;

                    case 8:
                        Eight();
                        break;

                    case 9:
                        exitFromProgram = Nine();
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion
        


        #region Отображение пунктов меню

        /// <summary>
        /// 1. Добавление в таблицу
        /// </summary>
        private void One()
        {
            #region красивое меню - 1
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("1. Ввод данных");
            Console.ResetColor();
            Console.WriteLine("                                      ║");

            Console.Write("║   2. Вывод таблицы на экран                                ║\n" +
                          "║   3. Удаление                                              ║\n" +
                          "║   4. Редактировать                                         ║\n" +
                          "║   5. Поиск                                                 ║\n" +
                          "║   6. Сортировка                                            ║\n" +
                          "║   7. Сохранение                                            ║\n" +
                          "║   8. Загрузка                                              ║\n" +
                          "║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion


            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }
            switch (button)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    reis.AddReise(Prices_List, Airport_List, Reises_List);
                    // Вызов метода добавления
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise = 9;
                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// 2. Вывод таблицы на экран
        /// </summary>
        private void Two()
        {
            #region красивое меню - 2
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("2. Вывод таблицы на экран");
            Console.ResetColor();
            Console.WriteLine("                           ║");

            Console.Write("║   3. Удаление                                              ║\n" +
                          "║   4. Редактировать                                         ║\n" +
                          "║   5. Поиск                                                 ║\n" +
                          "║   6. Сортировка                                            ║\n" +
                          "║   7. Сохранение                                            ║\n" +
                          "║   8. Загрузка                                              ║\n" +
                          "║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            switch (button)
            {
                case ConsoleKey.Enter: // выбор выделенного пункта меню
                    Console.Clear();
                    if (Reises_List.Count > 0)
                    {
                        reis.PrintReises(Reises_List);
                    }
                    else
                    {
                        Console.WriteLine("Список не заполнен!");
                        Console.ReadKey();
                    }
                    break;

                case ConsoleKey.Escape: // выход из проги
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow: // стрелочка вверх - идти выше
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow: // стрелочка вниз - идти ниже
                    MyChoise--;
                    break;

                default:
                    break;
            }


        }

        /// <summary>
        /// 3. Удаление
        /// </summary>
        private void Three()
        {
            #region красивое меню - 3
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n" +
                          "║   2. Вывод таблицы на экран                                ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("3. Удаление");
            Console.ResetColor();
            Console.WriteLine("                                         ║");

            Console.Write("║   4. Редактировать                                         ║\n" +
                          "║   5. Поиск                                                 ║\n" +
                          "║   6. Сортировка                                            ║\n" +
                          "║   7. Сохранение                                            ║\n" +
                          "║   8. Загрузка                                              ║\n" +
                          "║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            switch (button)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    if (Reises_List.Count > 0)
                    {
                        reis.DeleteInfo(Prices_List, Reises_List, prices);
                    }
                    else
                    {
                        Console.WriteLine("Список не заполнен!");
                        Console.ReadKey();
                    }
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise--;
                    break;

                default:
                    Thread.Sleep(300);
                    // снова печатать вариант 2 (Two)
                    break;
            }
        }

        /// <summary>
        /// 4. Редактировать
        /// </summary>
        private void Four()
        {
            #region красивое меню - 4
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n" +
                          "║   2. Вывод таблицы на экран                                ║\n" +
                          "║   3. Удаление                                              ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("4. Редактировать");
            Console.ResetColor();
            Console.WriteLine("                                    ║");

            Console.Write("║   5. Поиск                                                 ║\n" +
                          "║   6. Сортировка                                            ║\n" +
                          "║   7. Сохранение                                            ║\n" +
                          "║   8. Загрузка                                              ║\n" +
                          "║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            switch (button)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    reis.RedactInfo(Prices_List, Airport_List, Reises_List);
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise--;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 5. Поиск
        /// </summary>
        private void Five()
        {
            #region красивое меню - 5
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n" +
                          "║   2. Вывод таблицы на экран                                ║\n" +
                          "║   3. Удаление                                              ║\n" +
                          "║   4. Редактировать                                         ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("5. Поиск");
            Console.ResetColor();
            Console.WriteLine("                                            ║");

            Console.Write("║   6. Сортировка                                            ║\n" +
                          "║   7. Сохранение                                            ║\n" +
                          "║   8. Загрузка                                              ║\n" +
                          "║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            switch (button)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    reis.SearchInfo(airport, prices, Reises_List);
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise--;
                    break;

                default:
                    Thread.Sleep(300);
                    // снова печатать вариант 2 (Two)
                    break;
            }
        }

        /// <summary>
        /// 6. Сортировка
        /// </summary>
        private void Six()
        {
            #region красивое меню - 6
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n" +
                          "║   2. Вывод таблицы на экран                                ║\n" +
                          "║   3. Удаление                                              ║\n" +
                          "║   4. Редактировать                                         ║\n" +
                          "║   5. Поиск                                                 ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("6. Сортировка");
            Console.ResetColor();
            Console.WriteLine("                                       ║");

            Console.Write("║   7. Сохранение                                            ║\n" +
                          "║   8. Загрузка                                              ║\n" +
                          "║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            switch (button)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    //Reises_List = Reises_List.OrderBy(e => e.P_ReisCode.ReisCode).ToList();
                    reis.Sorting(ref Reises_List);
                    return;
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise--;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 7. Сохранение
        /// </summary>
        private void Seven()
        {
            #region красивое меню - 7
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n" +
                          "║   2. Вывод таблицы на экран                                ║\n" +
                          "║   3. Удаление                                              ║\n" +
                          "║   4. Редактировать                                         ║\n" +
                          "║   5. Поиск                                                 ║\n" +
                          "║   6. Сортировка                                            ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("7. Сохранение");
            Console.ResetColor();
            Console.WriteLine("                                       ║");

            Console.Write("║   8. Загрузка                                              ║\n" +
                          "║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            switch (button)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    reis.Saving(Prices_List, Airport_List, Reises_List);
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise--;
                    break;

                default:
                    Thread.Sleep(300);
                    break;
            }
        }

        /// <summary>
        /// 8. Загрузка
        /// </summary>
        private void Eight()
        {
            #region красивое меню - 8
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n" +
                          "║   2. Вывод таблицы на экран                                ║\n" +
                          "║   3. Удаление                                              ║\n" +
                          "║   4. Редактировать                                         ║\n" +
                          "║   5. Поиск                                                 ║\n" +
                          "║   6. Сортировка                                            ║\n" +
                          "║   7. Сохранение                                            ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("8. Загрузка");
            Console.ResetColor();
            Console.WriteLine("                                         ║");

            Console.Write("║   ESC. Выход                                               ║\n" +
                          "╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key;
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key;
            }

            switch (button)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    reis.Loading(ref Prices_List, ref Airport_List, ref Reises_List);
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    MyChoise = 9;
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise++;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise--;
                    break;

                default:
                    Thread.Sleep(300);
                    // снова печатать вариант 2 (Two)
                    break;
            }
        }

        /// <summary>
        /// 9. Выход
        /// </summary>
        private bool Nine()
        {
            #region красивое меню - 9
            Console.Write("╔════════════════════════════════════════════════════════════╗\n" + // 60
                          "║                        Главное меню                        ║\n" +
                          "╠════════════════════════════════════════════════════════════╣\n" +
                          "║   1. Ввод данных                                           ║\n" +
                          "║   2. Вывод таблицы на экран                                ║\n" +
                          "║   3. Удаление                                              ║\n" +
                          "║   4. Редактировать                                         ║\n" +
                          "║   5. Поиск                                                 ║\n" +
                          "║   6. Сортировка                                            ║\n" +
                          "║   7. Сохранение                                            ║\n" +
                          "║   8. Загрузка                                              ║\n");

            Console.Write("║   ---> ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write(" ESC. Выход");
            Console.ResetColor();
            Console.WriteLine("                                         ║");

            Console.Write("╚════════════════════════════════════════════════════════════╝");
            #endregion

            var button = Console.ReadKey(true).Key; // чтение клавиши

            // проверка на корректность
            while (button != ConsoleKey.Enter && button != ConsoleKey.Escape && button != ConsoleKey.DownArrow && button != ConsoleKey.UpArrow)
            {
                button = Console.ReadKey(true).Key; // исправление
            }

            switch (button) // что делать для определённой клавиши
            {
                case ConsoleKey.Enter: // выбор
                    Console.Clear();
                    return AreUSure("хотите выйти"); //
                    break;

                case ConsoleKey.Escape:
                    Console.Clear();
                    return AreUSure("хотите выйти");
                    break;

                case ConsoleKey.DownArrow:
                    MyChoise = 1;
                    break;

                case ConsoleKey.UpArrow:
                    MyChoise--;
                    break;

                default:
                    break;
            }
            return false;
        }
        #endregion
        




        #region Подтверждение выбора
        /// <summary>
        /// Вы уверены, что... - метод подтверждения
        /// </summary>
        /// <param name="text">ввод того, что подтвердить</param>
        /// <returns>ДА / НЕТ</returns>
        public static bool AreUSure(string text)
        {
            // выбор (от него зависит, что выведет)
            byte myChoise = 1;
            while (true) // цикл отображения ДА / НЕТ
            {
                Console.Clear();
                Console.WriteLine("Вы уверены, что " + text + "?"); 
                switch (myChoise) // что вывести в зависимости от значения myChoise
                {
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
                        else // нажата Enter
                           return true; // выход = ДА
                         
                        break;

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
                        else // нажата Enter
                           return false; // выход = НЕТ

                        break;

                    default:
                        break;
                }
               
            }

        }
        #endregion

    }
}
