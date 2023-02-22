using System;

namespace ConsoleCongratulator
{
    class Program
    {
        static void Main(string[] args)
        {

            Congratulator congratulator = new Congratulator();
            Console.WriteLine("Прошедшие дни рождения за 5 дней:");
            congratulator.ShowData(5, 0, "3");
            Console.WriteLine("Дни рождения за следующие 5 дней:");
            congratulator.ShowData(0, 5, "3");

            while (true)
            {
                Console.WriteLine(new string('=', 100));
                Console.WriteLine("Укажите действие:");
                Console.WriteLine("1 - Показать все данные (сортировка по Id)");
                Console.WriteLine("2 - Показать все данные (сортировка по алфавиту)");
                Console.WriteLine("3 - Показать все данные (сортировка по количеству дней до дня рождения");
                Console.WriteLine("4 - Задать диапазон дней и сортировку");
                Console.WriteLine("5 - Добавить новую запись");
                Console.WriteLine("6 - Редактировать запись");
                Console.WriteLine("7 - Удалить запись");

                string action = Console.ReadLine();

                try
                {
                    switch (action)
                    {
                        case "1":
                            congratulator.ShowData(183, 183, "1");
                            break;
                        case "2":
                            congratulator.ShowData(183, 183, "2");
                            break;
                        case "3":
                            congratulator.ShowData(183, 183, "3");
                            break;
                        case "4":
                            congratulator.ShowData();
                            break;
                        case "5":
                            congratulator.AddNewPerson();
                            break;
                        case "6":
                            congratulator.EditPerson();
                            break;
                        case "7":
                            congratulator.DeletePerson();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Ошибка, повторите действия");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Ошибка, Повторите действия");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }


            }


        }
    }
}

