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
        public int? ReisCode { get; set; }

        /// <summary>
        /// Кол-во мест бизнес класса (коды)
        /// </summary>
        public int? BuisnessClass_Num { get; set; }

        /// <summary>
        /// Цена за место - Бизнес
        /// </summary>
        public int? BuisnessClass_Price { get; set; }

        /// <summary>
        /// Кол-во мест эконом класса (коды)
        /// </summary>
        public int? EconomClass_Num { get; set; }

        /// <summary>
        /// Цена за место - Эконом
        /// </summary>
        public int? EconomClass_Price { get; set; }


        public List<Prices> Prices_List = new List<Prices>();


        #region Добавление рейса

        /// <summary>
        /// Добавление рейса + цены
        /// </summary>
        public void AddPriceForReis()
        { 
            // изначальные значения локальных переменных
            string input = "";

            int reisCode = 0, 
                buisnessClass_Num = 0, 
                buisnessClass_Price = 0, 
                economClass_Num = 0, 
                economClass_Price = 0;

            bool exit = false, allright = false; 

            while (!allright)
            {

                #region Ввод кода рейса (уникальный)
                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление стоимости билетов <---\n\n\n\n\n\n\n\n"); // вывод текста

                    Console.Write("Введите код рейса: ");
                    input = Console.ReadLine();
                    input = input.Trim();

                    if (string.IsNullOrWhiteSpace(input)) // проверка на корректность введённых данных
                    {
                        Console.WriteLine("\n\nПоле кода рейса не заполнено. Введите 3 цифры! \n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        Console.ReadKey();
                    }
                    else if (input.Length > 3)
                    {
                        Console.WriteLine("\n\nКод рейса слишком длинный. Введите 3 цифры! \n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        Console.ReadKey();
                    }
                    else if (input.Length < 3)
                    {
                        Console.WriteLine("\n\nКод рейса слишком короткий. Введите 3 цифры! \n" +
                                          "Нажмите любую кнопку, чтобы продолжить...");
                        Console.ReadKey();
                    }
                    else if (!int.TryParse(input, out reisCode))
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
                                exit = true; // если нет, то выходим и идём дальше
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

                    buisnessClass_Num = AddPlacesPrices(500, ref exit);
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

                    buisnessClass_Price = AddPlacesPrices(2000000, ref exit);

                }
                exit = false;
                #endregion

                #region Кол-во мест Эконом-Класса
                while (!exit)
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("---> Добавление стоимости билетов <---\n"); // вывод текста
                    Console.WriteLine("Код рейса: " + reisCode);
                    Console.WriteLine("Количество мест в Бизнес-Классе: " + buisnessClass_Num);
                    Console.Write("Цена за место в Бизнес-Классе: " + buisnessClass_Price + "\n\n\n\n\n");

                    Console.Write("Введите количество мест Эконом-Класса: ");

                    economClass_Num = AddPlacesPrices(500, ref exit);

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

                    economClass_Price = AddPlacesPrices(2000000, ref exit);

                }
                exit = false;
                #endregion

                //-------------------------------------------

                #region Вы уверены, что ввели верные данные? ДА/НЕТ

                allright = allright.AreUSure("---> Добавление стоимости билетов <---\n" +
                                             "\nКод рейса: " + reisCode +
                                             "\nКоличество мест в Бизнес-Классе: " + buisnessClass_Num +
                                             "\nЦена за место в Бизнес-Классе: " + buisnessClass_Price +
                                             "\nКоличество мест Эконом-Класса: " + economClass_Num +
                                             "\nЦена за место в Эконом-Классе: " + economClass_Price + "\n\n");
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

        
        // alreadyCreated
        #region Существует ли такой код рейса или нет

        /// <summary>
        /// Есть ли введённый код в списке?
        /// </summary>
        /// <param name="reisCode">Введённый код</param>
        /// <param name="priceList">Список кодов</param>
        /// <returns></returns>
        private static bool alreadyCreated(int number, List<Prices> priceList)
        {
            foreach (Prices pp in priceList)
            {
                if (pp.ReisCode == number)
                    return true;
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
        private int AddPlacesPrices(int maxNum, ref bool exit)
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


        public void PrintPrice(Prices price)
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
                              "║                                                      Таблица цен рейса                                              ║\n" +
                              "╠═══════════╦══════════════════════════╦═════════════════════════╦══════════════════════════╦═════════════════════════╣\n" +
                              "║ Код рейса ║ Код кресла Бизнес-класса ║ Стоимость Бизнес-класса ║ Код кресла Эконом-класса ║ Стоимость Эконом-класса ║\n" + 
                              "╠═══════════╬══════════════════════════╬═════════════════════════╬══════════════════════════╬═════════════════════════╣");
           Console.WriteLine($"║    {price.ReisCode}{new string (' ', 7- price.ReisCode.ToString().Length)}║         000-{price.BuisnessClass_Num:000}{new string (' ', 10)}║          {price.BuisnessClass_Price}p.{new string (' ', 12 - price.BuisnessClass_Price.ToString().Length)} ║         {price.BuisnessClass_Num:000}-{price.BuisnessClass_Num + price.EconomClass_Num:000}{new string(' ', 10)}║          {price.EconomClass_Price}p.{new string(' ', 12 - price.EconomClass_Price.ToString().Length)} ║");
            Console.WriteLine("╚═══════════╩══════════════════════════╩═════════════════════════╩══════════════════════════╩═════════════════════════╝\n");

            var press = Console.ReadKey(true).Key;
            while (press != ConsoleKey.Escape)
            {
                press = Console.ReadKey(true).Key;
            }
        }



    }
}
