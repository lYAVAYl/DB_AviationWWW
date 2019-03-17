using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{

    public class Prices
    {
        // код рейса | кол-во мест бизнес-класса (коды) | цена за место-Б | кол-во мест эконом-класса (коды) | цена за место-Э

        /// <summary>
        /// код рейса
        /// </summary>
        public string ReisCode { get; set; }

        /// <summary>
        /// Кол-во мест бизнес класса (коды)
        /// </summary>
        public int BuisnessClass_Num { get; set; }

        /// <summary>
        /// Цена за место - Бизнес
        /// </summary>
        public int BuisnessClass_Price { get; set; }

        /// <summary>
        /// Кол-во мест эконом класса (коды)
        /// </summary>
        public int EconomClass_Num { get; set; }

        /// <summary>
        /// Цена за место - Эконом
        /// </summary>
        public int EconomClass_Price { get; set; }


        public List<Prices> Prices_List = new List<Prices>();


        #region Добавление рейса

        /// <summary>
        /// Добавление рейса + цены
        /// </summary>
        public void AddPriceForReis()
        { 
            // изначальные значения локальных переменных
            string reisCode = ""; 
            int buisnessClass_Num = 0, buisnessClass_Price = 0, economClass_Num = 0, economClass_Price = 0;

            bool exit = false, allright = false;

            while (!allright)
            {

                #region Ввод кода рейса (уникальный)
                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление стоимости билетов <---\n\n\n\n\n\n\n\n"); // вывод текста

                    Console.Write("Введите код рейса: ");
                    reisCode = Console.ReadLine();
                    reisCode = reisCode.Trim();

                    if (string.IsNullOrWhiteSpace(reisCode)) // проверка на корректность введённых данных
                    {
                        Console.WriteLine("\n\nПоле кода рейса не заполнено. Введите 3 цифры! \n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        Console.ReadKey();
                    }
                    else if (reisCode.Length > 3)
                    {
                        Console.WriteLine("\n\nКод рейса слишком длинный. Введите 3 цифры! \n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        Console.ReadKey();
                    }
                    else if (reisCode.Length < 3)
                    {
                        Console.WriteLine("\n\nКод рейса слишком короткий. Введите 3 цифры! \n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        Console.ReadKey();
                    }
                    else if (!IsCorrectCode(reisCode))
                    {
                        Console.WriteLine("\n\nВ коде присутствуют символы запрещённые символы. Введите 3 цифры! \n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        Console.ReadKey();
                    }
                    else // код рейса введён верно
                    {
                        if (Prices_List.Count != 0) // если список кодов НЕ пуст
                        {
                            if (!alreadyCreated(reisCode, Prices_List)) // смотрим, есть ли уже такой код или нет
                            {
                                exit = true; // если нет, то ливаем и идём дальше
                            }
                            else // если да, выводим сообщение
                            {
                                Console.WriteLine("\n\nРейс с таким кодом уже существует! Заполните поле заново.\n" +
                                                  "Нажмите любую кнопку, чтобы продолжить...");
                                Console.ReadKey(true);
                            }
                        }
                        else // все проверки пройдены, а список пуст
                            exit = true; // то ливаем и идём дальше
                        
                    }
                }
                exit = false;
                #endregion


                #region Кол-во мест Бизнес-Класса
                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление стоимости билетов <---\n"); // вывод текста
                    Console.WriteLine("Код рейса: "+reisCode+ "\n\n\n\n\n\n");
                    Console.Write("Введите кол-во мест Бизнес-Класса: ");

                    buisnessClass_Num = CXC(500, ref exit);
                }
                exit = false;
                #endregion

                #region Цена за место в Бизнес-Классе
                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление стоимости билетов <---\n"); // вывод текста
                    Console.WriteLine("Код рейса: " + reisCode);
                    Console.Write("Количество мест в Бизнес-Классе: " + buisnessClass_Num + "\n\n\n\n\n\n");

                    Console.Write("Введите цену за место в Бизнес-Классе (руб): ");

                    buisnessClass_Price = CXC(2000000, ref exit);

                }
                exit = false;
                #endregion

                #region Кол-во месть Эконом-Класса
                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление стоимости билетов <---\n"); // вывод текста
                    Console.WriteLine("Код рейса: " + reisCode);
                    Console.WriteLine("Количество мест в Бизнес-Классе: " + buisnessClass_Num);
                    Console.Write("Цена за место в Бизнес-Классе: " + buisnessClass_Price + "\n\n\n\n\n");

                    Console.Write("Введите количество мест Эконом-Класса: ");

                    economClass_Num = CXC(500, ref exit);

                }
                exit = false;
                #endregion

                #region Цена за место в Эконом-Классе
                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление стоимости билетов <---\n"); // вывод текста
                    Console.WriteLine("Код рейса: " + reisCode);
                    Console.WriteLine("Количество мест в Бизнес-Классе: " + buisnessClass_Num);
                    Console.WriteLine("Цена за место в Бизнес-Классе: " + buisnessClass_Price);
                    Console.WriteLine("Количество мест Эконом-Класса: " + economClass_Num + "\n\n\n");

                    Console.Write("Введите цену за место в Эконом-Классе: ");

                    economClass_Price = CXC(2000000, ref exit);

                }
                exit = false;
                #endregion

                //-------------------------------------------

                #region Вы уверены, что ввели верные данные? ДА/НЕТ
                bool apply = false;
                while (!apply) // цикл отображения информации
                {
                    Airports.AreUSure("---> Добавление стоимости билетов <---\n" +
                                     "\nКод рейса: " + reisCode +
                                     "\nКоличество мест в Бизнес-Классе: " + buisnessClass_Num +
                                     "\nЦена за место в Бизнес-Классе: " + buisnessClass_Price +
                                     "\nКоличество мест Эконом-Класса: " + economClass_Num +
                                     "\nЦена за место в Эконом-Классе: " + economClass_Price + "\n\n", 
                                     ref allright, ref apply);
                }
                #endregion
            }
            Prices_List.Add(new Prices
            {
                ReisCode = reisCode,
                BuisnessClass_Num = buisnessClass_Num,
                BuisnessClass_Price = buisnessClass_Price,
                EconomClass_Num = economClass_Num,
                EconomClass_Price = economClass_Price
            } );
        }
        #endregion


        // IsCorrect
        #region Метод проверки на наличие "разрешённых символов" в строке

        /// <summary>
        /// Правильно и введён код?
        /// </summary>
        /// <param name="test">Введённый код</param>
        /// <returns></returns>
        private static bool IsCorrectCode(string test) // передача строки в метод
        {
            string legal = "1234567890"; // "разрешённые" символы

            bool ok = true; // верно или нет (по дефолту да)
            foreach(char c in test) // проверка, каждого символа из test на его наличие в списке 'разрешённых'
            {
                ok = legal.Contains(c); 
                if (ok == false)
                    break;
            }
            
            return ok; // возвращает true, если "запрещённых" символов нет
        }
        #endregion

        // alreadyCreated
        #region Существует ли такой код рейса или нет

        /// <summary>
        /// Есть ли введённый код в списке?
        /// </summary>
        /// <param name="reisCode">Введённый код</param>
        /// <param name="priceList">Список кодов</param>
        /// <returns></returns>
        private static bool alreadyCreated(string reisCode, List<Prices> priceList)
        {
            foreach (Prices pp in priceList)
            {
                if (pp.ReisCode == reisCode)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        // CXC
        #region добавление цены/кол-ва мест

        /// <summary>
        /// Добавление цены/кол-ва мест
        /// </summary>
        /// <param name="maxNum">Максимальное число</param>
        /// <param name="exit">условие выхода из внешнего цикла</param>
        /// <returns></returns>
        private int CXC(int maxNum, ref bool exit)
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
        #endregion

    }
}
