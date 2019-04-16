using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class Helper
    {
        // isBannedSymbolsInString
        #region Проверка на наличие запрещённых символов
        /// <summary>
        /// Проверка на наличие запрещённых символов
        /// </summary>
        /// <param name="testingString">проверяемая строка</param>
        /// <returns></returns>
        public static bool isBannedSymbolsInString(this string testingString)
        {
            string bannedSymbols = "0123456789!@\"#$%^&*_+=;<>?\\/|[]{}\t"; // запрещённые символы

            foreach (char symbol in testingString) // берём символ из строки
            {
                foreach (char ban in bannedSymbols) // берём символ из "запрещённых" символов
                {
                    if (symbol == ban) // сравниваем
                        return true; // возвращает false, если в строке есть "запрещённый" символ
                }
            }
            return false; // если запрещённых символов в строке нет, то возвращает true
        }
        #endregion
        
        // isBannedSymbolsInIATA
        #region Проверка на наличие запрещённых символов
        /// <summary>
        /// Проверка на наличие запрещённых символов
        /// </summary>
        /// <param name="testingString">проверяемая строка</param>
        /// <returns></returns>
        public static bool isBannedSymbolsInIATA(this string testingString)
        {
            string bannedSymbols = "0123456789!@\"'#$%^&*_+=-;<>?\\/|[]{}\t "; // запрещённые символы

            foreach (char symbol in testingString) // берём символ из строки
            {
                foreach (char ban in bannedSymbols) // берём символ из "запрещённых" символов
                {
                    if (symbol == ban) // сравниваем
                        return true; // возвращает false, если в строке есть "запрещённый" символ
                }
            }
            return false; // если запрещённых символов в строке нет, то возвращает true
        }
        #endregion
        
        // isBannedSymbolsInPlaneMark
        #region Проверка на наличие запрещённых символов
        /// <summary>
        /// Проверка на наличие запрещённых символов
        /// </summary>
        /// <param name="testingString">проверяемая строка</param>
        /// <returns></returns>
        public static bool isBannedSymbolsInPlaneMark(this string testingString)
        {
            string bannedSymbols = "!@\"#$%^&*_+=;<>?/|[]{}\t"; // запрещённые символы

            foreach (char symbol in testingString) // берём символ из строки
            {
                foreach (char ban in bannedSymbols) // берём символ из "запрещённых" символов
                {
                    if (symbol == ban) // сравниваем
                        return true; // возвращает false, если в строке есть "запрещённый" символ
                }
            }
            return false; // если запрещённых символов в строке нет, то возвращает true
        }
        #endregion


        //--------------------------------------------------

        // isCorrect
        #region Проверка введённой строки на корректность    
        /// <summary>
        /// Проверка строки на соответствие правилам ввода
        /// </summary>
        /// <param name="testingString">проверяемая строка</param>
        /// <param name="minLenght">минимальная длина</param>
        /// <param name="maxLenght">максимальная длина</param>
        /// <returns></returns>
        public static bool isCorrectString(this string testingString, int minLenght = 3, int maxLenght = 60)
        {
            if (string.IsNullOrWhiteSpace(testingString)) // пустая
            {
                Console.WriteLine("\n\nПоле не заполнено! Заполните поле.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.isBannedSymbolsInString()) // есть ли запрещённые символы
            {
                Console.WriteLine("\n\nВведённая строка содержит запрещённые символы. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.Length < minLenght) // меньше минимальной длины
            {
                Console.WriteLine("\n\nВведённая строка слишком короткая. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.Length > maxLenght) // больше максимальной длины
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

        // isCorrect_IATA
        #region Проверка введённой строки на корректность    
        /// <summary>
        /// Проверка строки на соответствие правилам ввода
        /// </summary>
        /// <param name="testingString">проверяемая строка</param>
        /// <param name="minLenght">минимальная длина</param>
        /// <param name="maxLenght">максимальная длина</param>
        /// <returns></returns>
        public static bool isCorrectIATA(this string testingString, int minLenght = 3, int maxLenght = 60)
        {
            if (string.IsNullOrWhiteSpace(testingString)) // пустая
            {
                Console.WriteLine("\n\nПоле не заполнено! Заполните поле.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.isBannedSymbolsInIATA()) // есть ли запрещённые символы
            {
                Console.WriteLine("\n\nВведённая строка содержит запрещённые символы. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.Length < minLenght) // меньше минимальной длины
            {
                Console.WriteLine("\n\nВведённая строка слишком короткая. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.Length > maxLenght) // больше максимальной длины
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

        // isCorrect_PlaneMark
        #region Проверка введённой строки на корректность    
        /// <summary>
        /// Проверка строки на соответствие правилам ввода
        /// </summary>
        /// <param name="testingString">проверяемая строка</param>
        /// <param name="minLenght">минимальная длина</param>
        /// <param name="maxLenght">максимальная длина</param>
        /// <returns></returns>
        public static bool isCorrectPlaneMark(this string testingString, int minLenght = 3, int maxLenght = 50)
        {
            if (string.IsNullOrWhiteSpace(testingString)) // пустая
            {
                Console.WriteLine("\n\nПоле не заполнено! Заполните поле.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.isBannedSymbolsInPlaneMark()) // есть ли запрещённые символы
            {
                Console.WriteLine("\n\nВведённая строка содержит запрещённые символы. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.Length < minLenght) // меньше минимальной длины
            {
                Console.WriteLine("\n\nВведённая строка слишком короткая. Заполните поле снова.\n" +
                                  "Нажмите любую кнопку, чтобы продолжить...");
                Console.ReadKey();
                return false;
            }
            else if (testingString.Length > maxLenght) // больше максимальной длины
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

        //--------------------------------------------------





        //--------------------------------------------------

        // AreUSure
        #region Вы уверены?

        /// <summary>
        /// Вы уверены, что всё ввели верно?
        /// </summary>
        /// <param name="info">выводит переданный текст</param>
        /// <param name="bigExit">условие выхода из цикла, который отвечает за изменение отображения таблички</param>
        /// <param name="smallExit">условие выхода из цикла, ответственного за добавление элемента</param>
        public static bool AreUSure(this bool answer, string infoUP="", string qa="")
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
                    Console.WriteLine("Вы уверены, что всё ввели верно?");
                }
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
                            //bigExit = true; // выход из 'большого' цикла
                            //smallExit = true; // выход из 'малого' цикла
                            return true;
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
                            //bigExit = false; // выход из 'большого' цикла
                            //smallExit = true; // выход из 'малого' цикла
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


        //--------------------------------------------------


        


        //--------------------------------------------------

    }
}
